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

  constructor(private _shareData: ShareDataService, @Inject(DOCUMENT) private document: Document) {

  }

  ngOnInit() {

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
}
