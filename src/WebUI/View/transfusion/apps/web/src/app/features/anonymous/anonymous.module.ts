import { NgModule } from '@angular/core';

import { AnonymousRoutingModule } from './anonymous-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { CenterSearchComponent } from './containers/center-search/center-search.component';
import { LoginComponent } from './containers/login/login.component';
import { DonorRegisterComponent } from './containers/donor-register/donor-register.component';

@NgModule({
  declarations: [
    CenterSearchComponent,
    LoginComponent,
    DonorRegisterComponent,
  ],
  imports: [SharedModule, AnonymousRoutingModule],
})
export class AnonymousModule {}
