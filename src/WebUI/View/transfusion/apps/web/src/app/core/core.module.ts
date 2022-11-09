import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiModule } from '../shared/transfusion-api-client/api.module';
import { Configuration } from '../shared/transfusion-api-client/configuration';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ApiModule.forRoot(() => new Configuration({})),
  ],
  exports: [
    ApiModule
  ]
})
export class CoreModule { }
