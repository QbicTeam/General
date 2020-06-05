import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-usersAdmin',
  templateUrl: './usersAdmin.component.html',
  styleUrls: ['./usersAdmin.component.css']
})
export class UsersAdminComponent implements OnInit {

  @Input() formData: any;
  
  constructor() { }

  ngOnInit() {
    console.log('Form container loaded...', this.formData);
  }

}
