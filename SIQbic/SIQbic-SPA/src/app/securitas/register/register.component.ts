import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserForLogin } from '../Dtos/UserForLogin';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() actionChange = new EventEmitter();
  registerForm: FormGroup;
  isLoading = false;
  user: UserForLogin = {UserName: "", Password: ""};

  constructor(private _authService: AuthService, private _alertify: AlertifyService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      displayname: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmpassword: new FormControl('', Validators.required)
    }, this.passwordMatchValidator);
  }

  actionChanged() {
    this.actionChange.emit("login");
  }

  register() {
    this.isLoading = true;

    this._authService.register(this.registerForm.value).subscribe(() => {
      this._alertify.success('Registered in successfully')
      this._authService.login({ "UserName": this.registerForm.value.username , "Password": this.registerForm.value.password}).subscribe(()=> {
        this.isLoading = false;
      }, error => {
        this._alertify.error(error);
        this.isLoading = false;
      })
    }, error => {
      this._alertify.error(error);
      this.isLoading = false;
    });

  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmpassword').value ? null : { 'mismatch': true };
  }

}

