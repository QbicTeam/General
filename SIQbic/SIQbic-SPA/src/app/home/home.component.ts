import { Component, OnInit } from '@angular/core';
import { AuthService } from '../securitas/_services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  currentRegCode: any;
  currentAction = "login";
  loggedIn = false;

  constructor(private _authService: AuthService, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._authService.currentAction.subscribe(action => {
      this.loggedIn = this._authService.loggedIn();
    });

    this.currentRegCode = this._route.snapshot.paramMap.get('rcode');
    if (this.currentRegCode)  {
      this.setAction("register");
    }

  }

  setAction(action) {
    this.currentAction = action;
  }

}
