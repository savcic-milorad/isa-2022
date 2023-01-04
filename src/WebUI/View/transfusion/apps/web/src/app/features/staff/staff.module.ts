import { NgModule } from '@angular/core';
import { StaffRoutingModule } from './staff-routing.module';
import { StaffHomepageComponent } from './containers/staff-homepage/staff-homepage.component';
import { SharedModule } from '../../shared/shared.module';
import { TermManagementComponent } from './containers/term-management/term-management.component';
import { StaffReservationsComponent } from './containers/staff-reservations/staff-reservations.component';

@NgModule({
  declarations: [
    StaffHomepageComponent,
    TermManagementComponent,
    StaffReservationsComponent
  ],
  imports: [SharedModule, StaffRoutingModule],
})
export class StaffModule { }
