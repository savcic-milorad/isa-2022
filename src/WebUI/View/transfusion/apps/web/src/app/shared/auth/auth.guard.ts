import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthFacade } from './auth.facade.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanLoad {

  constructor(private authFacade: AuthFacade, private router: Router) {
  }

  canLoad(route: Route, segments: UrlSegment[]): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    console.log(`canLoad guard - route.path: ${route.path}`);
    console.log(`canLoad guard - segments[0]: ${segments[0]}`);
    const role = this.authFacade.getRouteForAssignedRole();
    if(role === segments[0].path) {
      return true;
    }

    const redirectToActualRole = this.router.createUrlTree([`/${role}`]);
    return redirectToActualRole;
  }
}
