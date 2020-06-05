import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CoreService {

constructor() { }

  getCompanyList() {
    return [
      { "id": 1, "displayName": "Qbic", "logo": "../../assets/img/LogoQbic.jpg"},
      { "id": 2, "displayName": "Pizza Hut", "logo": "../../assets/img/phlogo1.png"},
      { "id": 3, "displayName": "Starbuks", "logo": "../../assets/img/sblogo2.png"}
    ]
  }

  getMenuByCompany() {

    return [
      { "id": 1, "displayName": "CORE", "displayIcon": "", "url":"", "childs":[
        { "id": 2, "displayName": "Dashboard", "displayIcon": "fa fa-home", "url":"/home", "childs":[] }
      ] },
      { "id": 3, "displayName": "INTERFACE", "displayIcon": "", "url":"", "childs":[
        { "id": 4, "displayName": "Layouts", "displayIcon": "fa fa-columns", "url":"", "childs":[
          { "id": 5, "displayName": "Static Navigation", "displayIcon": "", "url":"/home", "childs":[] },
          { "id": 6, "displayName": "Light Sidenav", "displayIcon": "", "url":"/home", "childs":[] }
        ] },
        { "id": 7, "displayName": "Pages", "displayIcon": "fa fa-book", "url":"", "childs":[
          { "id": 8, "displayName": "Authentication", "displayIcon": "", "url":"/home", "childs":[] },
          { "id": 9, "displayName": "Error", "displayIcon": "", "url":"/home", "childs":[] }
        ] }
      ] },
      { "id": 13, "displayName": "SiQbic", "displayIcon": "", "url":"", "childs":[
        { "id": 14, "displayName": "Seguridad", "displayIcon": "fa fa-columns", "url":"", "childs":[
          { "id": 15, "displayName": "Nuevos Usuarios", "displayIcon": "", "url":"/form/users", "childs":[] },
          { "id": 16, "displayName": "Asignacion de Persmisos", "displayIcon": "", "url":"/home", "childs":[] }
        ] }
      ] },
      { "id": 10, "displayName": "ADDONS", "displayIcon": "", "url":"", "childs":[
        { "id": 11, "displayName": "Charts", "displayIcon": "fa fa-bars", "url":"/home", "childs":[] },
        { "id": 12, "displayName": "Tables", "displayIcon": "fa fa-table", "url":"/home", "childs":[] }
      ] }

    ]

  }


  getFormInfoByName(formName: string) {

    return {
      "formName": "Registro de Nuevos Usuarios",
      "path": ["SiQbic", "Seguridad", "Nuevos Usuarios"],
      "notes": "Esta forma solo sirve para hacer la invitacion a nuevos usuarios",
      "notesType": "alert-info",
      "formTitle": "Invitacion de nuevos usuarios",
      "formIcon": "fa fa-user-plus",
      "footerNotes": "La tasa de usuarios usuarios que responden a la ivitacion por correo es del 80%",
      "componente":""
    }
  }

}
