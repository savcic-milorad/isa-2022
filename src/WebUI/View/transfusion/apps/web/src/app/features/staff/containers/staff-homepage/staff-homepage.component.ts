import { Component } from '@angular/core';
import { AuthFacade } from '../../../../shared/auth/auth.facade.service';

@Component({
  selector: 'transfusion-staff-homepage',
  templateUrl: './staff-homepage.component.html',
  styleUrls: ['./staff-homepage.component.scss'],
})
export class StaffHomepageComponent {
  logoutPossible: boolean;

  constructor(private authFacade: AuthFacade) {
    this.logoutPossible = authFacade.isLogoutPossible();
  }

  logout() {
    this.authFacade.logout();
  }
}
