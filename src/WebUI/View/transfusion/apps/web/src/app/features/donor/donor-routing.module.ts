import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DonorProfileComponent } from './containers/donor-profile/donor-profile.component';
import { DonorComponent } from './containers/donor/donor.component';

const routes: Routes = [
  { path: '', component: DonorComponent },
  { path: 'profile', component: DonorProfileComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DonorRoutingModule { }
