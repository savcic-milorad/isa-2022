import { Injectable } from "@angular/core";
import { AuthState } from "./auth.state.service";

@Injectable({
  providedIn: 'root'
})
export class AuthFacade {

  constructor(private authState: AuthState) {
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
    return this.authState.removeAccessToken();
  }

  isLogoutPossible() {
    return this.authState.isLogoutPossible();
  }
}
