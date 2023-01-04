import { Component } from '@angular/core';
import { AuthFacade } from '../../../../shared/auth/auth.facade.service';

@Component({
  selector: 'transfusion-administrator-homepage',
  templateUrl: './administrator-homepage.component.html',
  styleUrls: ['./administrator-homepage.component.scss'],
})
export class AdministratorHomepageComponent {
  logoutPossible: boolean;

  constructor(private authFacade: AuthFacade) {
    this.logoutPossible = authFacade.isLogoutPossible();
  }

  logout() {
    this.authFacade.logout();
  }
}
