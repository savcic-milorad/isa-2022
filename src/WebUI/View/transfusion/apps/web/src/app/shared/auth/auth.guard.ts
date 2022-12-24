import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthFacade } from './auth.facade.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanLoad, CanActivate, CanActivateChild {

  constructor(private authFacade: AuthFacade, private router: Router) {
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    // console.log(`canActivate guard - route: ${route}`);
    // console.log(`canActivate guard - route.url[0]: ${route.url[0]}`);
    // const role = this.authFacade.getRouteForAssignedRole();
    // if(role === route.url[0].path) {
    //   return true;
    // }

    // const isValidAccessToken = true;
    // if(isValidAccessToken) {
    //   return true;
    // }

    // const redirectToActualRole = this.router.createUrlTree([`/${role}`]);
    // return redirectToActualRole;
    return true;
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    const role = this.authFacade.getRouteForAssignedRole();
    // console.log(`canActivateChild guard - childRoute.pathFromRoot: ${childRoute.pathFromRoot}`);
    // console.log(`canActivateChild guard - childRoute.url[0]: ${childRoute.url[0]}`);
    console.log(`canActivateChild guard - role: ${role}`);

    return true;
  }

  canLoad(route: Route, segments: UrlSegment[]): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    const role = this.authFacade.getRouteForAssignedRole();

    console.log(`canLoad guard - route.path: ${route.path}`);
    console.log(`canLoad guard - segments[0]: ${segments[0]}`);
    console.log(`canLoad guard - role: ${role}`);

    // Route and role match, continue
    if (role === segments[0].path) {
      console.log(`canLoad guard - all ok, continue`);
      return true;
    }

    // Access token invalid, redirect to base
    const isValidAccessToken = this.authFacade.isValidAccessToken();
    if (!isValidAccessToken) {
      console.log(`canLoad guard - invalid token, redirecting to base: ${role}`);
      const redirectToBaseUrl = this.router.createUrlTree([`/`])
      return redirectToBaseUrl;
    }

    // Redirect to base of role
    const redirectToActualRole = this.router.createUrlTree([`/${role}`]);
    console.log(`canLoad guard - redirecting to actual role: ${role}`);
    return redirectToActualRole;;
  }
}
