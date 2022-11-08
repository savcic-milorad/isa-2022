import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DonorRoutingModule } from './donor-routing.module';
import { DonorProfileComponent } from './containers/donor-profile/donor-profile.component';
import { DonorComponent } from './containers/donor/donor.component';

@NgModule({
  declarations: [DonorComponent, DonorProfileComponent],
  imports: [CommonModule, DonorRoutingModule],
})
export class DonorModule {}
