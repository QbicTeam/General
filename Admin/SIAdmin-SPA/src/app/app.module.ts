import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MaindashboardComponent } from './maindashboard/maindashboard.component';
import { ConfiguracionbdComponent } from './configuracionbd/configuracionbd.component';
import { SidebarComponent } from './home/sidebar/sidebar.component';
import { NavbarComponent } from './home/navbar/navbar.component';
import { FooterComponent } from './home/footer/footer.component';
import { BusinessComponent } from './configuracionbd/business/business.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      SidebarComponent,
      NavbarComponent,
      FooterComponent,
      MaindashboardComponent,
      ConfiguracionbdComponent,
      BusinessComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      FormsModule,
      HttpClientModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
