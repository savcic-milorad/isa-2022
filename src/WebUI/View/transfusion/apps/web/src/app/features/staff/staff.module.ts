import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffRoutingModule } from './staff-routing.module';
import { StaffHelloComponent } from './containers/staff-hello/staff-hello.component';

@NgModule({
  declarations: [StaffHelloComponent],
  imports: [CommonModule, StaffRoutingModule],
})
export class StaffModule {}
