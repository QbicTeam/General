import { Component, OnInit } from '@angular/core';
import { Business } from '../_model/business';
import { AdminService } from '../_services/admin.service';

@Component({
  selector: 'app-configuracionbd',
  templateUrl: './configuracionbd.component.html',
  styleUrls: ['./configuracionbd.component.css']
})
export class ConfiguracionbdComponent implements OnInit {
  business = new Array<any>();
  customers: any;
  customerSelected: any;
  customerSelectedDetail: any;

  constructor(private _service: AdminService) { }

  ngOnInit() {
    this.loadCustomers();
  }

  loadCustomers(){
    this._service.getCustormerList().subscribe(data => {
      console.log('Data', data);
      this.customers = data;
    });
  }

  onCustomerChange() {
    console.log('CustomerSelected', this.customerSelected);
    this._service.getCustomerDetails(this.customerSelected.clienteId, this.customerSelected.actualizacionId).subscribe(data => {
      console.log('CustomerData', data);
      this.customerSelectedDetail = data;
    });
    
 }

  startProcess(value) {
    
    this.business.push(new Business(this.customerSelectedDetail.clienteId,'BD Control',true,'','','','',''));
    this.business.push(new Business(this.customerSelectedDetail.clienteId,'Negocio 1',false,'','','','',''));

  }

  private checkIfPendiengBusiness(): boolean {
    const rst = this.business.find(x => !x.isExecuted);
    if (rst) {
      return true;
    } else {
      return false;
    }
  }

  onBusinessChange(business: Business) {
    let b = this.business.find(x => x.business === business.business);
    if (b) {
      b.isExecuted = true;
    }
    if (!this.checkIfPendiengBusiness()) {
      this._service.updateCustomerUpdate(this.customerSelected.clienteId, this.customerSelected.actualizacionId).subscribe(data => {
        console.log('CustomerData', data);
        this.customerSelectedDetail = data;
      });
    }

    if (business.isDBControl) {
      // TODO Correo de Alta del Portal.
      this._service.createInvitedUser(this.customerSelectedDetail.cliente.email).subscribe((code) => {
        console.log('sending invittion...', code);
        this._service.sendInvitationEmail("Gerardo Cabrera", code).subscribe(() => {
          
          console.log('Invitacion Created');
        });
      });

    } else {
      // TODO Correo de Alta de Negocio.
    }
  }

}
