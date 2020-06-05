import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { Business } from 'src/app/_model/business';

@Component({
  selector: 'app-business',
  templateUrl: './business.component.html',
  styleUrls: ['./business.component.css']
})
export class BusinessComponent implements OnInit {

  @Input() currentBusinnes: Business;
  @Output() executedChange = new EventEmitter();

  isSaving = false;
  isExecuted = false;

  constructor(private _adminService: AdminService) { }

  ngOnInit() {
  }
  
  onSave() {
    this.isSaving = true;
    console.log('currentBusiness: ', this.currentBusinnes);

    this._adminService.saveBusiness(this.currentBusinnes).subscribe(() => {
      this.isSaving = false;
      console.log('Saved');
      this.isExecuted = true;
      this.executedChange.emit(this.currentBusinnes);


    });
    
    
  }
}
