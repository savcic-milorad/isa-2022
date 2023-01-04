import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NavigationComponent } from './containers/navigation/navigation.component';
import { PageNotFoundComponent } from './containers/page-not-found/page-not-found.component';
import { RouterModule } from '@angular/router';

const modules = [CommonModule, MaterialModule, ReactiveFormsModule];

const components = [NavigationComponent, PageNotFoundComponent];

@NgModule({
  declarations: [NavigationComponent, PageNotFoundComponent],
  imports: [RouterModule, ...modules],
  exports: [...components, ...modules],
})
export class SharedModule {}
