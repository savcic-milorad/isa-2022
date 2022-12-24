import { Component } from '@angular/core';
import { AuthFacade } from '../../../../shared/auth/auth.facade.service';

@Component({
  selector: 'transfusion-anonymous-home',
  templateUrl: './anonymous-home.component.html',
  styleUrls: ['./anonymous-home.component.scss'],
})
export class AnonymousHomeComponent {
  logoutPossible: boolean;

  constructor(private authFacade: AuthFacade) {
    this.logoutPossible = authFacade.isLogoutPossible();
  }

  logout(event: string) {
    this.authFacade.logout();
  }
}
