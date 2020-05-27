import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { UserForRegister } from '../Dtos/UserForRegister';
import { UserForLogin } from '../Dtos/UserForLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.securitasApiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  private actionSource = new BehaviorSubject<string>("");
  currentAction = this.actionSource.asObservable();

  constructor(private _http: HttpClient) { }

  register(user:UserForRegister) {
    return this._http.post(this.baseUrl + "register", user);
  }

  login(user: UserForLogin) {
    return this._http.post(this.baseUrl + "login", user)
      .pipe(
        map((response: any) => {
          const user = response;

          if (user) {
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.notifyAction("loggedin");
          }
        })
      );
  }

  loggedIn() {
    const token = localStorage.getItem("token");
    return !!token;
  }

  logout() {
     localStorage.removeItem("token");
     this.notifyAction("loggedOut");
  }

  private notifyAction(action: string) {
    this.actionSource.next(action);
  }

}
