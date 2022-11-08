import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { CoreModule } from './core/core.module';
import { LandingComponent } from './features/landing/landing.component';
import { ApiModule, Configuration } from './shared/transfusion-api-client';

@NgModule({
  declarations: [LandingComponent],
  imports: [
    BrowserModule, 
    AppRoutingModule, 
    CoreModule, 
    ApiModule.forRoot(() => new Configuration({}))
  ],
  providers: [],
  bootstrap: [LandingComponent]
})
export class AppModule {}
