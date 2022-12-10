import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StaffHelloComponent } from './containers/staff-hello/staff-hello.component';

const routes: Routes = [
  { path: '', component: StaffHelloComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaffRoutingModule { }
