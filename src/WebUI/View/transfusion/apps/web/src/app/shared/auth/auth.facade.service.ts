import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AuthState } from "./auth.state.service";

@Injectable({
  providedIn: 'root'
})
export class AuthFacade {

  constructor(private authState: AuthState, private router: Router) {
  }

  saveAccessToken(accessToken: string) {
    this.authState.setAccessToken(accessToken);
  }

  getAccessToken(): string {
    return this.authState.getAccessToken();
  }

  getRouteForAssignedRole(): string {
    return this.authState.getRouteForAssignedRole();
  }

  logout() {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/']);
    });
    this.authState.removeAccessToken();
  }

  isLogoutPossible() {
    return this.authState.isLogoutPossible();
  }

  isValidAccessToken(): boolean {
    const accessToken = this.getAccessToken();
    return accessToken !== undefined && accessToken !== null && accessToken !== '';
  }
}
