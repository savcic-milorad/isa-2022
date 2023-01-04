import { Component } from '@angular/core';
import { AuthFacade } from '../../../../shared/auth/auth.facade.service';

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

  logout() {
    this.authFacade.logout();
  }
}
