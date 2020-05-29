import { Component, OnInit } from '@angular/core';
import { AuthService } from '../securitas/_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  currentAction = "login";
  loggedIn = false;

  constructor(private _authService: AuthService) { }

  ngOnInit() {
    this._authService.currentAction.subscribe(action => {
      this.loggedIn = this._authService.loggedIn();
    });

  }

  setAction(action) {
    this.currentAction = action;
  }

}
