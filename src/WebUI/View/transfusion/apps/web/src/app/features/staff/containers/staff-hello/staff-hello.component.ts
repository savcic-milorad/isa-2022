import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { StaffFacade } from '../../staff.facade';

@Component({
  selector: 'transfusion-staff-hello',
  templateUrl: './staff-hello.component.html',
  styleUrls: ['./staff-hello.component.scss'],
})
export class StaffHelloComponent {

  isLoading$: Observable<boolean>;
  hasSaidHello$: Observable<boolean>;

  constructor(private staffFacade: StaffFacade) {
    this.isLoading$ = staffFacade.isLoading$();
    this.hasSaidHello$ = staffFacade.hasSaidHello$();
  }

  sayHello() {
    this.staffFacade.sayHello();
  }
}
