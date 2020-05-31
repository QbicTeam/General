import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { MaindashboardComponent } from './maindashboard/maindashboard.component';
import { HomeComponent } from './home/home.component';
import { SecuritasModule } from './securitas/securitas.module';
import { AlertifyService } from './_services/alertify.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { UsersAdminComponent } from './Forms/Security/usersAdmin/usersAdmin.component';
import { HeaderFormComponent } from './Forms/headerForm/headerForm.component';
import { ContainerFormComponent } from './Forms/containerForm/containerForm.component';
import { AuthGuard } from './_guards/auth.guard';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      SidebarComponent,
      FooterComponent,
      MaindashboardComponent,
      HomeComponent,
      UsersAdminComponent,
      HeaderFormComponent,
      ContainerFormComponent,
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      SecuritasModule,
      FileUploadModule
   ],
   providers: [
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
