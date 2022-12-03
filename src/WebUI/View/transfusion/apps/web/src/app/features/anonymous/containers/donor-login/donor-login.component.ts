import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { AnonymousFacade } from '../../anonymous.facade';

@Component({
  selector: 'transfusion-donor-login',
  templateUrl: './donor-login.component.html',
  styleUrls: ['./donor-login.component.scss'],
})
export class DonorLoginComponent {
  
  hidePassword = true;
  hideConfirmPassword = true;
  isLoading$: Observable<boolean>;
  loginForm;
  
  constructor(private fb: FormBuilder, private anonymousFacade: AnonymousFacade) {
    this.isLoading$ = anonymousFacade.isLoading$();

    this.loginForm = this.fb.nonNullable.group({
      emailAddress: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(8)])]
    });
  }

  onLoginSubmit() {
    console.log(`Submitted login for ${this.loginForm.value}`);
    // this.anonymousFacade.
  }
}
