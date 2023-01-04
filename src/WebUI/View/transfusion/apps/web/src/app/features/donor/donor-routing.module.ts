import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../shared/auth/auth.guard';
import { DonorHomepageComponent } from './containers/donor-homepage/donor-homepage.component';
import { DonorProfileComponent } from './containers/donor-profile/donor-profile.component';
import { DonorQuestionnaireComponent } from './containers/donor-questionnaire/donor-questionnaire.component';
import { DonorReservationOverviewComponent } from './containers/donor-reservation-overview/donor-reservation-overview.component';
import { DonorReservationSearchComponent } from './containers/donor-reservation-search/donor-reservation-search.component';

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
        path: 'search',
        component: DonorReservationSearchComponent
      },
      {
        path: 'reservations',
        component: DonorReservationOverviewComponent
      },
      {
        path: 'questionnaire',
        component: DonorQuestionnaireComponent
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
