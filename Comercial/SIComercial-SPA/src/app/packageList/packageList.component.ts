import { Component, OnInit, Input } from '@angular/core';
import { ComercialService } from '../_services/comercial.service';
import {  OwlOptions } from 'ngx-owl-carousel-o';


@Component({
  selector: 'app-packageList',
  templateUrl: './packageList.component.html',
  styleUrls: ['./packageList.component.css']
})
export class PackageListComponent implements OnInit {
  @Input() itemsList: any[];

  constructor(private _comercialService: ComercialService) { }

  packages;

  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: true,
    dots: true,
    navSpeed: 700,
    navText: ['<<', '>>'],
    responsive: {
      0: {
        items: 1
      },
      400: {
        items: 2
      },
      740: {
        items: 3
      },
      940: {
        items: 4
      }
    },
    nav: true
  };


 
  ngOnInit() {
    console.log('Paquetes para Carrucel', this.itemsList);
    // this.loadPackages();
  }

  // loadPackages() {
    
  // this._comercialService.getPaquetes().subscribe(data => {
  //     this.packages = data;
  //     console.log('Paquetes', this.packages);
  //     // this.selectPackage();
  //   });
    
  // }

}
