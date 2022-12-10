import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../shared/auth/auth.guard';
import { DonorHomepageComponent } from './containers/donor-homepage/donor-homepage.component';
import { DonorProfileComponent } from './containers/donor-profile/donor-profile.component';

const routes: Routes = [
  {
    path: '',
    component: DonorHomepageComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'profile'
      },
      {
        path: 'profile',
        component: DonorProfileComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DonorRoutingModule { }
