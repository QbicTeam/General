import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContainerFormComponent } from './Forms/containerForm/containerForm.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
      path: '',
      children: [
        { path: 'home/:rcode', component: HomeComponent },
        { path: 'home', component: HomeComponent },
        { path: 'form/users', component: ContainerFormComponent, canActivate: [AuthGuard] }
      ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full'}];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
