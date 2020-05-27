import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

constructor(private _http: HttpClient) { }

  sendEmailInvitationForRegister() {
    const url = "http://localhost:5001/emails/";

    var req = {	
      "To": "majahide.payan@hotmail.com",
      "Body": "",
      "Subject": "Please Register in SiQbic - Start Now.",
      "IsHtml": true,
      "TemplateId": 48
    };

    return this._http.post(url, req,  this.getHeader());
  }

  private getHeader() {

    const httpOptions = {
      headers: new HttpHeaders({
        "x-api-key": "420a7b1a-447b-4d9d-a9f5-7a8d04053c38"
      })
    };
  
    return httpOptions;
  }
  
}
