import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AdministratorFacade } from '../../administrator.facade';

@Component({
  selector: 'transfusion-administrator-hello',
  templateUrl: './administrator-hello.component.html',
  styleUrls: ['./administrator-hello.component.scss'],
})
export class AdministratorHelloComponent {

  isLoading$: Observable<boolean>;
  hasSaidHello$: Observable<boolean>;

  constructor(private administratorFacade: AdministratorFacade) {
    this.isLoading$ = administratorFacade.isLoading$();
    this.hasSaidHello$ = administratorFacade.hasSaidHello$();
  }

  sayHello() {
    console.log('INVOKED');
    this.administratorFacade.sayHello();
  }
}
