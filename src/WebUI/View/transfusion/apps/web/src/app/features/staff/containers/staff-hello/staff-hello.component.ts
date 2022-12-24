import { Component } from '@angular/core';
import { merge, Observable, tap } from 'rxjs';
import { StaffFacade } from '../../staff.facade';
import { AdministratorFacade } from '../../../administrator/administrator.facade';

@Component({
  selector: 'transfusion-staff-hello',
  templateUrl: './staff-hello.component.html',
  styleUrls: ['./staff-hello.component.scss'],
})
export class StaffHelloComponent {

  isLoading$: Observable<boolean>;
  hasSaidHello$: Observable<boolean>;
  hasAdministratorSaidHello$: Observable<boolean>;

  constructor(private staffFacade: StaffFacade, private administratorFacade: AdministratorFacade) {
    const hasStaffSaidHello$ = staffFacade.hasSaidHello$();
    const isLoadingStaff$ = staffFacade.isLoading$();

    const hasAdministratorSaidHello$ = this.administratorFacade.hasSaidHello$();
    const isLoadingAdministrator$ = this.administratorFacade.isLoading$();

    this.isLoading$ = merge(isLoadingAdministrator$, isLoadingStaff$);
    this.hasSaidHello$ = merge(hasAdministratorSaidHello$, hasStaffSaidHello$).pipe(tap(val => console.log(`TAP ${val}`)));
    this.hasAdministratorSaidHello$ = hasAdministratorSaidHello$;
  }

  sayHello() {
    this.staffFacade.sayHello();
  }

  sayAdministratorHello() {
    this.administratorFacade.sayHello();
  }
}
