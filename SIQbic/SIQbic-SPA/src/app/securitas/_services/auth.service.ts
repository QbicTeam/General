import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  baseEmailsUrl = environment.emailsApiUrl;

  jwtHelper = new JwtHelperService();
  decodedToken: any;

  private actionSource = new BehaviorSubject<string>("");
  currentAction = this.actionSource.asObservable();

  constructor(private _http: HttpClient) { }

  register(user:UserForRegister) {
    return this._http.post(this.baseUrl + "register", user);
  }

  updateUser(id: any, userForUpdate: any) {
    return this._http.put(this.baseUrl + "users/" + id, userForUpdate);
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

  getUserSettings(userId: any) {
    return this._http.get(this.baseUrl + "users/" + userId + "/settings");
  }

  getQuestions() {
    return this._http.get(this.baseUrl + "questions");
  }

  getDecodedToken() {
    const token = localStorage.getItem("token");
    return this.jwtHelper.decodeToken(token);
  }

  loggedIn() {
    const token = localStorage.getItem("token");
    return !!token;
  }

  logout() {
     localStorage.removeItem("token");
     this.notifyAction("loggedOut");
  }

  startOnBoardProcess(regCode: string) {
    return this._http.get(this.baseUrl + "onboard/" + regCode); 
  }

  requestInvitation(regCode: string) {
    return this._http.post(this.baseUrl + "invites/" + regCode, null); 
  }

  getInvitations() {
    return this._http.get(this.baseUrl + "invites");
  }

  updateInvitation(id: any, roleId: any, invitedName: any) {
    return this._http.put(this.baseUrl + "invites/" + id, {roleId: roleId, invitedName: invitedName});
  }

  cancellInvitation(id: any) {
    return this._http.delete(this.baseUrl + "invites/" + id);
  }

  approveInvitation(id: any) {
    return this._http.put(this.baseUrl + "invites/" + id + "/approve", null);
  }

  createInvitation(invitation: any) {
    return this._http.post(this.baseUrl + "invites", invitation);
  }

  sendInvitationEmail(email: any) {
    return this._http.post(this.baseEmailsUrl + "emails", email, this.getHeader());
  }

  private notifyAction(action: string) {
    this.actionSource.next(action);
  }

  private getHeader() {

    const httpOptions = {
      headers: new HttpHeaders({
        "x-api-key": environment.emailSupportApiKey
      })
    };

    return httpOptions;
  }

}
