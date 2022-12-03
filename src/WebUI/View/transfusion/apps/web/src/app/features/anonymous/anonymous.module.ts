import { NgModule } from '@angular/core';

import { AnonymousRoutingModule } from './anonymous-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { CenterSearchComponent } from './containers/center-search/center-search.component';
import { DonorRegisterComponent } from './containers/donor-register/donor-register.component';
import { DonorLoginComponent } from './containers/donor-login/donor-login.component';

@NgModule({
  declarations: [
    CenterSearchComponent,
    DonorRegisterComponent,
    DonorLoginComponent,
  ],
  imports: [SharedModule, AnonymousRoutingModule],
})
export class AnonymousModule {}
