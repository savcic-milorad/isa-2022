import { NgModule } from '@angular/core';

import { DonorRoutingModule } from './donor-routing.module';
import { DonorProfileComponent } from './containers/donor-profile/donor-profile.component';
import { DonorHomepageComponent } from './containers/donor-homepage/donor-homepage.component';
import { SharedModule } from '../../shared/shared.module';
import { DonorReservationSearchComponent } from './containers/donor-reservation-search/donor-reservation-search.component';
import { DonorReservationOverviewComponent } from './containers/donor-reservation-overview/donor-reservation-overview.component';
import { DonorQuestionnaireComponent } from './containers/donor-questionnaire/donor-questionnaire.component';

@NgModule({
  declarations: [
    DonorProfileComponent,
    DonorHomepageComponent,
    DonorReservationSearchComponent,
    DonorReservationOverviewComponent,
    DonorQuestionnaireComponent,
  ],
  imports: [SharedModule, DonorRoutingModule],
})
export class DonorModule {}
