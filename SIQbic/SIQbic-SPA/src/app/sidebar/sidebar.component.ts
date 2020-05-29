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

    console.log(this._authService.decodedToken);

    this._authService.currentAction.subscribe(action => {
      this.loggedIn = this._authService.loggedIn();
    });

    this._shareDataService.companyDataSource.subscribe(action => {

      if (!action)
        return;
        
      if (action.action == ActionType.Selected) {
        this.loadMenuByCompanyId(action.id);
      }
    });  
  }

  loadMenuByCompanyId(id: number) {
    
    this.menu = this._coreService.getMenuByCompany(id);
    console.log("Loaded menu for company:  " + id, this.menu);

  }



}


//{{_authService.decodedToken?.given_name | titlecase }}