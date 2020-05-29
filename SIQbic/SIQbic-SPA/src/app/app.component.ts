import { Component, OnInit, Inject } from '@angular/core';
import { ShareDataService } from './_services/ShareData.service';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SIQbic-SPA';
  sidebarVissible = true;

  constructor(private _shareData: ShareDataService, @Inject(DOCUMENT) private document: Document) {

  }

  ngOnInit() {

    this._shareData.currentActionSource.subscribe(action => {
      if (action == "sidebarToogle")
        this.sidebarVissible = !this.sidebarVissible;


      if (this.sidebarVissible) {
        this.document.body.classList.remove('sb-sidenav-toggled');
      } else {
        this.document.body.classList.add('sb-sidenav-toggled');
      }
    });

  }
}
