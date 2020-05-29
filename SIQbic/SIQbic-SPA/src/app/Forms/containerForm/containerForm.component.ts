import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/_services/core.service';

@Component({
  selector: 'app-containerForm',
  templateUrl: './containerForm.component.html',
  styleUrls: ['./containerForm.component.css']
})
export class ContainerFormComponent implements OnInit {

  currentForm: any;
    
  constructor(private _coreService: CoreService) { }

  ngOnInit() {
    this.loadSelectedFormData();
  }

  loadSelectedFormData() {
    this.currentForm = this._coreService.getFormInfoByName("[selected form name from uri]");
  }

}
