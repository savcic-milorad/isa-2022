import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginCommand } from '@transfusion/transfusion-api-client';
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
    const loginCommand: LoginCommand = {
      userName: this.loginForm.controls.emailAddress.value ?? '',
      password: this.loginForm.controls.password.value ?? ''
    };

    this.anonymousFacade.login(loginCommand);
  }
}
