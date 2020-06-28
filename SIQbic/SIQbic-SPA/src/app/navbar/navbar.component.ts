import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CoreService } from '../_services/core.service';
import { AuthService } from '../securitas/_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { ShareDataService } from '../_services/ShareData.service';
import { DataSource } from '../_model/DataSource';
import { ActionType } from '../_model/ActionType.enum';
import { QuickAction } from '../_model/QuickAction';
import { PhotoEditorComponent } from '../securitas/photo-editor/photo-editor.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @ViewChild(PhotoEditorComponent) photoEditor: PhotoEditorComponent;
  @ViewChild('cmdCloseUpdateUser') closeBtnApprove: ElementRef;
  loggedIn = false;
  companies: any;
  companySelected: any;
  sidebarVisible = true;
  notifications: any[];
  currentUser: any;
  currentSection = 'general';
  selectedQ1: any;
  selectedQ2: any;
  selectedQ3: any;
  cusrrentPasswordChanges: any ={
    currentPwd: "",
    newPwd: "",
    confirmPwd: ""
  }

  currentUserSettings: any;

  constructor(public _authService: AuthService, private _coreService: CoreService, private _alertify: AlertifyService, 
    private _router: Router, private _shareData: ShareDataService) { }


  ngOnInit() {
    
    this._authService.currentAction.subscribe(action => {
      this.loggedIn = this._authService.loggedIn();
      if (this.loggedIn)
      {
        this.loadCompanies();
        this.currentUser = this._authService.getDecodedToken();
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

    if (this.cusrrentPasswordChanges?.currentPwd || this.cusrrentPasswordChanges?.newPwd || this.cusrrentPasswordChanges?.confirmPwd) {
      if (this.cusrrentPasswordChanges?.newPwd != this.cusrrentPasswordChanges?.confirmPwd) {
        this._alertify.error('La nueva clave de acceso y la confirmacion no son iguales.');
        return;
      }
    }

    this.currentUserSettings.responses[0].questionId = this.selectedQ1.id;
    this.currentUserSettings.responses[1].questionId = this.selectedQ2.id;
    this.currentUserSettings.responses[2].questionId = this.selectedQ3.id;

    if (this.photoEditor) {
      this.photoEditor.onUpload();
    } else {
      this.onSuccesPhoto(null);
    }
    
  }

  onSuccesPhoto(url) {

    if (url) {
      this.currentUserSettings.photoUrl = url;
    }

    
    const userForUpdate = {
      "displayName": this.currentUserSettings.displayName,
      "photoUrl": this.currentUserSettings.photoUrl,
      "currentPwd": this.cusrrentPasswordChanges.currentPwd,
      "newPwd": this.cusrrentPasswordChanges.newPwd,
      "responses": this.currentUserSettings.responses
    }


    this._authService.updateUser(this.currentUser.nameid, userForUpdate).subscribe(response => {
      
      if (response["msg"] == "") {
        this._alertify.success("Usuario Actualizado");
      } else {
        this._alertify.warning(response["msg"])
      }
      
      
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
    this._shareData.notifyActionSource(new QuickAction("sidebarToogle", { value: this.sidebarVisible}));
  }

  onCurrentSection(sectionName) {
    this.currentSection = sectionName;
  }

  loadUserSettings() {
    
    this._authService.getUserSettings(this.currentUser.nameid).subscribe(response => {

      this.currentUserSettings = response;
      this.selectedQ1 = this.currentUserSettings.questions.find(q => q.id == this.currentUserSettings.responses[0].questionId);
      this.selectedQ2 = this.currentUserSettings.questions.find(q => q.id == this.currentUserSettings.responses[1].questionId);
      this.selectedQ3 = this.currentUserSettings.questions.find(q => q.id == this.currentUserSettings.responses[2].questionId);

    });
  }

}
