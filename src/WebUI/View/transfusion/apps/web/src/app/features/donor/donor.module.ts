import { NgModule } from '@angular/core';

import { DonorRoutingModule } from './donor-routing.module';
import { DonorProfileComponent } from './containers/donor-profile/donor-profile.component';
import { DonorHomepageComponent } from './containers/donor-homepage/donor-homepage.component';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
    declarations: [DonorProfileComponent, DonorHomepageComponent],
    imports: [SharedModule, DonorRoutingModule]
})
export class DonorModule {}
