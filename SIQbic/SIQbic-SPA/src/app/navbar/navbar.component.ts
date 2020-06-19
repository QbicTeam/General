import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CoreService } from '../_services/core.service';
import { AuthService } from '../securitas/_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { ShareDataService } from '../_services/ShareData.service';
import { DataSource } from '../_model/DataSource';
import { ActionType } from '../_model/ActionType.enum';
import { QuickAction } from '../_model/QuickAction';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @ViewChild('cmdCloseUpdateUser') closeBtnApprove: ElementRef;
  loggedIn = false;
  companies: any;
  companySelected: any;
  sidebarVisible = true;
  notifications: any[];
  currentUser: any;

  constructor(public _authService: AuthService, private _coreService: CoreService, private _alertify: AlertifyService, 
    private _router: Router, private _shareData: ShareDataService) { }


  ngOnInit() {
    
    this._authService.currentAction.subscribe(action => {
      this.loggedIn = this._authService.loggedIn();
      if (this.loggedIn)
      {

        this.loadCompanies();

        this.currentUser = this._authService.getDecodedToken();
        console.log(this.currentUser, this.currentUser.uri);
      }
    
    });


    this._shareData.liveNotificationChange.subscribe(msg => {
      if (msg)
      {
        if (!this.notifications) this.notifications = new Array();
        
        this.notifications.push(msg);
      }
    });

  }

  onUpdateUser() {
    this._authService.updateUser(this.currentUser.nameid, {"displayName": this.currentUser.given_name}).subscribe(() => {
      this._alertify.success("Usuario Actualizado");
      this.closeBtnApprove.nativeElement.click();
    }, err => {
      this._alertify.error(err);
    });
  }

  logout() {
    this._authService.logout();
    this._alertify.message("Logged out");    
    this._router.navigate(['/home']);
    this._shareData.notifyActionSource(new QuickAction("sidebarToogle", { value: false}));
  }

  onCompanyChange() {
    this._shareData.notifyCompanyDataSource(new DataSource(this.companySelected.id, ActionType.Selected, "", this.companySelected));
  }

  private loadCompanies() {

    this.companies = this._coreService.getCompanyList();
    this.companySelected = this.companies[0];
    this._shareData.notifyCompanyDataSource(new DataSource(this.companies[0].id, ActionType.Selected, "", this.companies[0]));

  }

  onSidebarToggle() {
    
    this.sidebarVisible = !this.sidebarVisible;

    console.log(new QuickAction("sidebarToogle", { value: this.sidebarVisible}));

    this._shareData.notifyActionSource(new QuickAction("sidebarToogle", { value: this.sidebarVisible}));
  }

}
