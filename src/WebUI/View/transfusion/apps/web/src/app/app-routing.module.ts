import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'anonymous', loadChildren: () => import('./features/anonymous/anonymous.module').then(m => m.AnonymousModule) },
  { path: 'donor', loadChildren: () => import('./features/donor/donor.module').then(m => m.DonorModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
