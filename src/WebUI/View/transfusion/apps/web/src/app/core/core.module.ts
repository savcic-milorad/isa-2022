import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiModule } from '../shared/transfusion-api-client/api.module';
import { Configuration } from '../shared/transfusion-api-client/configuration';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    ApiModule.forRoot(() => new Configuration({}))
  ],
  exports: [
    ApiModule
  ]
})
export class CoreModule { }
