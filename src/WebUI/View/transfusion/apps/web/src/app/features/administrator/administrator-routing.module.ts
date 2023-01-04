import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../shared/auth/auth.guard';
import { AdministratorHomepageComponent } from './containers/administrator-homepage/administrator-homepage.component';
import { AdministratorManagementComponent } from './containers/administrator-management/administrator-management.component';
import { CenterManagementComponent } from './containers/center-management/center-management.component';
import { StaffManagementComponent } from './containers/staff-management/staff-management.component';

const routes: Routes = [
  {
    path: '',
    component: AdministratorHomepageComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'center-management'
      },
      {
        path: 'center-management',
        component: CenterManagementComponent
      },
      {
        path: 'staff-management',
        component: StaffManagementComponent
      },
      {
        path: 'administrator-management',
        component: AdministratorManagementComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministratorRoutingModule { }
