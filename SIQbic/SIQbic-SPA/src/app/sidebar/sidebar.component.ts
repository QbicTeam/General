import { Component, OnInit } from '@angular/core';
import { CoreService } from '../_services/core.service';
import { AuthService } from '../securitas/_services/auth.service';
import { ShareDataService } from '../_services/ShareData.service';
import { ActionType } from '../_model/ActionType.enum';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  loggedIn = false;
  showSideBar = true;

  menu: any;

  constructor(private _coreService: CoreService, public _authService: AuthService, private _shareDataService : ShareDataService) { }

  ngOnInit() {

    this.loadMenu();

    this._authService.currentAction.subscribe(action => {
      this.loggedIn = this._authService.loggedIn();
    });

  }

  loadMenu() {
    
    this.menu = this._coreService.getMenuByCompany();
 
  }



}


//{{_authService.decodedToken?.given_name | titlecase }}