import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnonymousHomeComponent } from './containers/anonymous-home/anonymous-home.component';
import { CenterSearchComponent } from './containers/center-search/center-search.component';
import { LoginComponent } from './containers/login/login.component';
import { DonorRegisterComponent } from './containers/donor-register/donor-register.component';

const routes: Routes = [
  {
    path: '',
    component: AnonymousHomeComponent,
    children: [
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'donor-register',
        component: DonorRegisterComponent
      },
      {
        path: 'center-search',
        component: CenterSearchComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AnonymousRoutingModule { }
