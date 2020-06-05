import { Component, OnInit } from '@angular/core';
import { ComercialService } from '../_services/comercial.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  currentStep = 1;
  isRegistered = false;
  currentPackage;
  confirmedCode: string;
  packages;
  currentCustomer = {
    "name": "",
    'phone': '',
    'email': '',
    'package': this.currentPackage,
    'companyName': '',
    'shortName': '',
    'rfc': '',
    'address': ''
  };

  constructor(private _comercialService: ComercialService) { }

  ngOnInit() {
    this.loadPackages();
  }

  loadPackages() {
    
  this._comercialService.getPaquetes().subscribe(data => {
      this.packages = data;
      console.log('Paquetes', this.packages);
      this.selectPackage();
    });
    
  }

  selectPackage() {
    const pos = Math.round(Math.random() * (this.packages.length - 0) + 0);
    this.currentPackage = this.packages[pos];
  }

  register() {
    
    this.currentCustomer.package = this.currentPackage;
    console.log('Register customer...', this.currentCustomer);
    this.isRegistered = true;

    console.log('Customer', this.currentCustomer);

    // let customer = this.currentCustomer;
    // let cliente = {
    //   "Contacto": customer.name, //"El Contacto del Segundo Cliente",
    //   "Telefono": customer.phone, //"664 765 4321",
    //   "email": customer.email, //"CarlosSotoOcio@gmail.com",
    //   "NomEmpresa": customer.companyName, //"Desarrollos Ecologicos SA de CV",
    //   "NomCorto": customer.shortName, //"ECO",
    //   "RFC": customer.rfc, //"ECO200530TR2",
    //   "Domicilio": customer.address, //"Calle Agregricultura #102, Frac. Siempre Verde",
    //   "PaqueteId": customer.package.id //2
    //   };

    //   console.log('Cliente',cliente)
    this._comercialService.saveCustomer(this.currentCustomer).subscribe(() => {
      console.log('Customer Saved');
      this._comercialService.sendWelcomeEmail(this.currentCustomer.name, this.currentCustomer.email).subscribe(() =>{
        console.log('Welcome Email sent...');
      });
  
      this._comercialService.sendSupportOrder(this.currentCustomer.name).subscribe(() => {
        console.log('Work Order sent...');
      });
    });


  }
  
  onPackageSelect(pkg) {
    this.currentPackage = pkg;
  }

  nextStep() {
    
    this.currentStep ++;

    console.log('current step: ', this.currentStep, this.currentCustomer);

    if (this.currentStep == 2) {
      this._comercialService.requestConfirmationCode(this.currentCustomer.name, this.currentCustomer.email).subscribe(()=> {
        console.log('confirmation code sent');
      });
    }

    if (this.currentStep == 3) {
      this._comercialService.validateConfirmationCode(this.currentCustomer.email, this.confirmedCode);
    }
  }

}
