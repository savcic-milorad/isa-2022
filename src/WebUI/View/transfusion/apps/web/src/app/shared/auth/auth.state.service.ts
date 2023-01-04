import { Injectable } from "@angular/core";
import { TransfusionApiAccessTokenClaims } from "./models/transfusion-api-access-token-claims.model";



@Injectable({
  providedIn: 'root'
})
export class AuthState {
  private readonly accessTokenLocalStorageKey: string = 'accessToken';

  removeAccessToken() {
    localStorage.removeItem(this.accessTokenLocalStorageKey);
  }

  isLogoutPossible() {
    const accessTokenValue = localStorage.getItem(this.accessTokenLocalStorageKey) ?? '';
    const isAccessTokenValueValid = accessTokenValue !== '';
    return isAccessTokenValueValid;
  }

  setAccessToken(accessToken: string) {
    localStorage.removeItem(this.accessTokenLocalStorageKey);
    localStorage.setItem(this.accessTokenLocalStorageKey, accessToken);
  }

  getAccessToken(): string {
    const accessTokenValue = localStorage.getItem(this.accessTokenLocalStorageKey) ?? '';
    return accessTokenValue;
  }

  getRouteForAssignedRole(): string {
    const accessToken = this.getAccessToken();
    const claims = this.extractClaimsFromBase64EncodedToken(accessToken);
    const roleLowerCase = claims.role.toLowerCase();

    // console.log(`Assigned role from persisted access token: ${roleLowerCase}`);

    return roleLowerCase;
  }

  private extractClaimsFromBase64EncodedToken(token: string): TransfusionApiAccessTokenClaims {
    try {
      const tokenContent = token.split(".")[1];
      const decodedTokenContent = atob(tokenContent);
      const tokenClaims = JSON.parse(decodedTokenContent);
      return tokenClaims;
    } catch (e) {

      console.warn(`Could not parse access token`);
      return {
        role: 'anonymous',
        username: '',
        userId: '',
        nbf: '',
        exp: '',
        iss: '',
        aud: ''
      };
    }
  };
}
