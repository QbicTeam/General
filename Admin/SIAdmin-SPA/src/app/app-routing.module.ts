import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ConfiguracionbdComponent } from './configuracionbd/configuracionbd.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  {
      path: '',
      children: [
          { path: 'home', component: HomeComponent },
          { path: 'configuracion', component: ConfiguracionbdComponent }
      ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
