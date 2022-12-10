import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdministratorHelloComponent } from './containers/administrator-hello/administrator-hello.component';

@NgModule({
  declarations: [AdministratorHelloComponent],
  imports: [CommonModule, AdministratorRoutingModule],
})
export class AdministratorModule {}
