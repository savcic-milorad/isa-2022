import { NgModule } from '@angular/core';
import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdministratorHomepageComponent } from './containers/administrator-homepage/administrator-homepage.component';
import { StaffManagementComponent } from './containers/staff-management/staff-management.component';
import { CenterManagementComponent } from './containers/center-management/center-management.component';
import { AdministratorManagementComponent } from './containers/administrator-management/administrator-management.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [
    AdministratorHomepageComponent,
    StaffManagementComponent,
    CenterManagementComponent,
    AdministratorManagementComponent
  ],
  imports: [SharedModule, AdministratorRoutingModule],
})
export class AdministratorModule { }
