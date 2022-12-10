import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministratorHelloComponent } from './containers/administrator-hello/administrator-hello.component';

const routes: Routes = [
  { path: '', component: AdministratorHelloComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministratorRoutingModule { }
