import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared/auth/auth.guard';

const routes: Routes = [
  {
    path: 'anonymous',
    loadChildren: () => import('./features/anonymous/anonymous.module').then(m => m.AnonymousModule),
    canLoad: [AuthGuard]
  },
  {
    path: 'donor',
    loadChildren: () => import('./features/donor/donor.module').then(m => m.DonorModule),
    canLoad: [AuthGuard]
  },
  {
    path: 'administrator',
    loadChildren: () => import('./features/donor/donor.module').then(m => m.DonorModule),
    canLoad: [AuthGuard]
  },
  {
    path: 'staff',
    loadChildren: () => import('./features/donor/donor.module').then(m => m.DonorModule),
    canLoad: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
