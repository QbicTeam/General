import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserForLogin } from '../Dtos/UserForLogin';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  isSubmitted = false;
  isCodeValid = false;

  
  @Output() actionChange = new EventEmitter();
  currentRegCode: any;
  registerForm: FormGroup;
  isLoading = false;
  errorMsg = "";
  errorCode = "";
  user: UserForLogin = {UserName: "", Password: ""};

  questions1: any;
  questions2: any;
  questions3: any;

  // questions1 = [
  //   "Cual es el nombre de su primer mascota?",
  //   "Donde estudio la primaria?",
  //   "Cual es color favorito?",
  //   "Tipo de carro preferido?"
  // ];
  // questions2 = [
  //   "Cual es el nombre de su primer mascota?",
  //   "Donde estudio la primaria?",
  //   "Cual es color favorito?",
  //   "Tipo de carro preferido?"
  // ];
  // questions3 = [
  //   "Cual es el nombre de su primer mascota?",
  //   "Donde estudio la primaria?",
  //   "Cual es color favorito?",
  //   "Tipo de carro preferido?"
  // ];

  constructor(private _authService: AuthService, private _alertify: AlertifyService, private _route: ActivatedRoute, private router: Router) { }

  ngOnInit() {

    this.loadQuestions();

    this.currentRegCode = this._route.snapshot.paramMap.get('rcode');
    if (this.currentRegCode)  {
      this.startOnBoard(this.currentRegCode);
    } 

    this.registerForm = new FormGroup({
      registrationCode: new FormControl('', Validators.required),
      username: new FormControl('', Validators.required),
      displayname: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confirmpassword: new FormControl('', Validators.required),
      response1: new FormControl('', Validators.required),
      response2: new FormControl('', Validators.required),
      response3: new FormControl('', Validators.required),
      question1: new FormControl('', Validators.required),
      question2: new FormControl('', Validators.required),
      question3: new FormControl('', Validators.required)
    }, [this.passwordMatchValidator, this.compareQuestions]);
  }

  startOnBoard(regCode: string) {
    this.errorMsg = "";
    this._authService.startOnBoardProcess(regCode).subscribe(data => {
      this.isCodeValid = true;
      this.registerForm.controls["username"].setValue(data["email"]);
      this.registerForm.controls["registrationCode"].setValue(regCode);
    }, error => {
      this.errorMsg = error;
      this.errorCode = this.errorMsg.substr(0,3);
      this.isCodeValid = true;
    });
  }

  actionChanged() {
    this.actionChange.emit("login");
  }

  register() {
    this.isSubmitted = true;

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

  compareQuestions(g: FormGroup) {

    let result = false;

    const q1 = g.get('question1').value;
    const q2 = g.get('question2').value;
    const q3 = g.get('question3').value;

    if (q1 === q2)
      result = true;

    if (q1 === q3)
      result = true;

    if (q2 === q3)
      result = true;

    return !result ? null: { 'qequals': true };
  }

  get question1() {
    return this.registerForm.get('question1');
  }

  get question2() {
    return this.registerForm.get('question2');
  }

  get question3() {
    return this.registerForm.get('question3');
  }


  onChangeQuestion1(e) {
    this.question1.setValue(e.target.value);
  }

  onChangeQuestion2(e) {
    this.question2.setValue(e.target.value);
  }

  onChangeQuestion3(e) {
    this.question3.setValue(e.target.value);
  }

  requestInvitation() {
    this._authService.requestInvitation(this.currentRegCode).subscribe(() => {
      this._alertify.success("Invitacion Solicitada, Espere notificacion por correo.");
      this.router.navigate(['/home']);
    }, error => {
      this._alertify.error(error);
    });
  }

  cancelInvitation() {
    this.router.navigate(['/home']);
  }


  private loadQuestions() {
    this._authService.getQuestions().subscribe(data => {
      this.questions1 = data;
      this.questions2 = data;
      this.questions3 = data;
    });
  }



}

