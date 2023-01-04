import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../shared/auth/auth.guard';
import { StaffHomepageComponent } from './containers/staff-homepage/staff-homepage.component';
import { StaffReservationsComponent } from './containers/staff-reservations/staff-reservations.component';
import { TermManagementComponent } from './containers/term-management/term-management.component';

const routes: Routes = [
  {
    path: '',
    component: StaffHomepageComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'reservations'
      },
      {
        path: 'reservations',
        component: StaffReservationsComponent
      },
      {
        path: 'term-management',
        component: TermManagementComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaffRoutingModule { }
