import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'transfusion-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {

  @Input() logoutPossible: boolean;
  @Output() logout = new EventEmitter<string>();

  constructor() {
    this.logoutPossible = false;
  }

  emitLogout() {
    const msg = 'logout called';
    console.log('[NavigationComponent] Logout emitted from navigation component with message: ' + msg);
    this.logout.emit(msg);
  }

  // login() {
  //   this.router.navigate(['/anonymous/donor'])
  //   { path: 'login', component: DonorLoginComponent },
  //   { path: 'donor-register', component: DonorRegisterComponent },
  // }

  // register() {

  // }
}
