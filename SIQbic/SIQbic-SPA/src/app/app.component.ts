import { Component, OnInit, Inject } from '@angular/core';
import { ShareDataService } from './_services/ShareData.service';
import { DOCUMENT } from '@angular/common';
import { AuthService } from './securitas/_services/auth.service';
import * as signalR from '@aspnet/signalr';
import { AlertifyService } from './_services/alertify.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SIQbic-SPA';
  private _hubConnection: signalR.HubConnection;

  constructor(private _shareData: ShareDataService, @Inject(DOCUMENT) private document: Document, 
    public _authService: AuthService, private _alertify: AlertifyService) {
    
  }

  ngOnInit() {

    this.buildConnection();
    this.startConnection();

    let sidebarVisible = false;

    this._shareData.currentActionSource.subscribe(action => {

      if (action && action != null && action.key == "sidebarToogle")
        sidebarVisible = action.value.value;

      if (sidebarVisible) {
        this.document.body.classList.remove('sb-sidenav-toggled');
      } else {
        this.document.body.classList.add('sb-sidenav-toggled');
      }
    });

  }

  //TODO: Remove hardcoded Url
  private buildConnection() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://majahide-001-site1.itempurl.com/releasecandidates/sites/livenotifications/liveNotificationsHub")
      .build();
  }

  private startConnection() {
    this._hubConnection.start().then(() => {
      this._alertify.success("Live Notifications started");
      this.registerSignalEvents();
    })
    .catch(err => {
      this._alertify.error("Live Notication connection fail...");
    });
  }

  private registerSignalEvents() {
    this._hubConnection.on("sendMessage", (action, msg) => {
      this._alertify.message(msg);
      this._shareData.notifyLiveNotification(msg);
    });
  }
}
