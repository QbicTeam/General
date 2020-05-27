import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  currentAction = "login";
  loggedIn = false;

  constructor() { }

  ngOnInit() {
  }

  setAction(action) {
    this.currentAction = action;
  }


}
