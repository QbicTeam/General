import { Component, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {
  @Output() nuevoClienteChange = new EventEmitter();

   isSaving = false;
   isRequested = false;

  constructor() { }

  ngOnInit() {
  }

  onNuevoCliente() {
    this.nuevoClienteChange.emit(null);
    this.isRequested = true;

  }
}
