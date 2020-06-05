import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Business } from '../_model/business';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

constructor(private _http: HttpClient) { }
  baseURL = environment.apiURL;
  apiSI_URL = environment.apiSI_URL;

  getCustormerList() {
    return this._http.get(this.baseURL + 'clientes/Actualizaciones/1');
  }

  getCustomerDetails(customerId, requestId) {
    return this._http.get(this.baseURL + 'clientes/' + customerId + '/Actualizaciones/' + requestId);
  }

  saveBusiness(business: Business) {
    let uri = this.baseURL + 'clientes/' + business.customerId + '/negocio';
    console.log('saveBusiness');
    console.log('path', uri);
    console.log('business', business);

    let negocio = {
      Server: business.server,
      DataBase: business.db,
      UserDB: business.userDB,
      PWD: business.password,
      Puerto: business.port,
      NombreNegocio: business.business,
      NegocioID: 1,
      IsDBControl: business.isDBControl,
      ClienteId: business.customerId,
    };
    console.log('negocio', negocio);


    return this._http.post(uri, negocio);
  }

  updateCustomerUpdate(customerId, requestId) {
    let update = { Status: 2 };
    return this._http.put(this.baseURL + 'clientes/' + customerId + '/Actualizaciones/' + requestId, update);
  }

  createInvitedUser(emailUser) {
    let invite = {
      "RoleId":1,
      "InvitedEmail": emailUser,
      "SponsorEmail":"support@qsg.mx"
      };
    return this._http.post(this.apiSI_URL + 'invites', invite);
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
