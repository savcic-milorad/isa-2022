import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiModule } from './transfusion-api-client/api.module';

const modules = [
  CommonModule,
  ApiModule
];

@NgModule({
  declarations: [],
  imports: [
    ...modules
  ],
  exports: [
    ...modules
  ]
})
export class SharedModule { }
