import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-headerForm',
  templateUrl: './headerForm.component.html',
  styleUrls: ['./headerForm.component.css']
})
export class HeaderFormComponent implements OnInit {

  @Input() formData: any;

  constructor() { }

  ngOnInit() {
    console.log('Form heder loaded...', this.formData);
  }

}
