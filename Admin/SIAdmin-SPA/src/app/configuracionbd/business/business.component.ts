import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-business',
  templateUrl: './business.component.html',
  styleUrls: ['./business.component.css']
})
export class BusinessComponent implements OnInit {

  isSaving = false;

  constructor(private _adminService: AdminService) { }

  ngOnInit() {
  }
  
  onSave() {
    
    this._adminService.sendEmailInvitationForRegister().subscribe(() => {
      console.log('saved... and invitation sent...');
    });
  }
}
