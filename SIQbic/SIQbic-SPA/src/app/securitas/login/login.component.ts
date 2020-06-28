import { Component, OnInit, EventEmitter, Output} from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserForLogin } from '../Dtos/UserForLogin';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { QuickAction } from 'src/app/_model/QuickAction';
import { ShareDataService } from 'src/app/_services/ShareData.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  @Output() actionChange = new EventEmitter();

  isLoading = false;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  user: UserForLogin = {UserName: "", Password: ""};

  constructor(private _authService: AuthService, private _router: Router, private _alertify: AlertifyService, private _shareData: ShareDataService) { }

  ngOnInit(): void {
  }

  actionChanged() {
    this.actionChange.emit("register");
  }

  login() {
    this.isLoading = true;
    this._authService.login(this.user).subscribe((data) => {
      this._shareData.notifyActionSource(new QuickAction("sidebarToogle", { value: true}));
      this._alertify.success('Logged in successfully')
      this.isLoading = false;
    }, error => {
      this._alertify.error(error);
      this.isLoading = false;
    });
  }

  onResetPwd() {
    console.log('Reseting pwd.');
  }

}
