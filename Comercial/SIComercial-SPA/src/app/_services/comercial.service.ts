import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ComercialService {

constructor(private _http: HttpClient) { }

  getPaquetes() {

    var result = new Array();

    result.push({
      Id: 1,
      ClaseIcono: "fa fa-map",
      Titulo: "Terrenos"
    });

    result.push({
      Id: 2,
      ClaseIcono: "fa fa-home",
      Titulo: "Vivienda"
    });

    result.push({
      Id: 3,
      ClaseIcono: "fa fa-money",
      Titulo: "Rentas"
    });

    return result;

  }

  requestConfirmationCode(name: string, email: string) {

    const url = "http://localhost:5001/emails/";

    var req = {
      "To":email,
      "Body": "",
      "Subject": "Hi " + name + " - Confirmation Code",
      "IsHtml": true,
      "TemplateId": 45,
      "Values": [ { "Variable":"NOMBRE_CLIENTE", "Value":name }, { "Variable":"CODIGO_CONFIRMACION", "Value":"2005252154" }]
    }

    return this._http.post(url, req,  this.getHeader());

  }

  sendWelcomeEmail(name: string, email: string) {

    const url = "http://localhost:5001/emails/";

    var req = {
      "To":email,
      "Body": "",
      "Subject": "Welcome " + name,
      "IsHtml": true,
      "TemplateId": 46,
      "Values": [ { "Variable":"nombre_cliente", "Value":name }]
    }

    return this._http.post(url, req,  this.getHeader());

  }

  sendSupportOrder(name: string) {
    const url = "http://localhost:5001/emails/";

    var req = {	
      "To": "majahide.payan@hotmail.com",
      "Body": "",
      "Subject": "Order request from " + name,
      "IsHtml": true,
      "TemplateId": 47
    };

    return this._http.post(url, req,  this.getHeader());

  }

  validateConfirmationCode(email: string, code: string) {
    console.log('confirming code...', email, code);
    return true;
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
