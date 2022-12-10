import { Component } from '@angular/core';
import { AuthFacade } from 'apps/web/src/app/shared/auth/auth.facade.service';

@Component({
  selector: 'transfusion-donor-homepage',
  templateUrl: './donor-homepage.component.html',
  styleUrls: ['./donor-homepage.component.scss'],
})
export class DonorHomepageComponent {

  logoutPossible: boolean;

  constructor(private authFacade: AuthFacade) {
    this.logoutPossible = authFacade.isLogoutPossible();
  }

  logout(event: string) {
    console.log('[DoonorHomepageComponent] Event received in parent: ' + event);
    this.authFacade.logout();
  }
}
