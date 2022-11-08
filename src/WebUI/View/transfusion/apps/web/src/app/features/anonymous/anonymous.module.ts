import { NgModule } from '@angular/core';

import { AnonymousRoutingModule } from './anonymous-routing.module';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    AnonymousRoutingModule
  ]
})
export class AnonymousModule { }
