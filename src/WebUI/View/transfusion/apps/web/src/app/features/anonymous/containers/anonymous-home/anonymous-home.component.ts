import { Component } from '@angular/core';
import { AuthFacade } from 'apps/web/src/app/shared/auth/auth.facade.service';

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
    throw new Error('Logout should not be possible from the anonymous module: ' + event);
  }
}
