import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
// eslint-disable-next-line @nrwl/nx/enforce-module-boundaries
import { Sex } from 'apps/web/src/app/shared/transfusion-api-client/model/sex';
import { AnonymousFacade } from '../../anonymous.facade';

@Component({
  selector: 'transfusion-donor-register',
  templateUrl: './donor-register.component.html',
  styleUrls: ['./donor-register.component.scss'],
})
export class DonorRegisterComponent {

  passwordPattern = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/i;

  hidePassword = true;
  hideConfirmPassword = true;
  registerForm;
  loginForm;
  isLoading$;

  constructor(private fb: FormBuilder, private anonymousFacade: AnonymousFacade) {
    this.isLoading$ = anonymousFacade.isLoading$();

    this.registerForm = this.fb.nonNullable.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      sex: [Sex.Female, Validators.required],
      jmbg: ['', Validators.required],
      state: ['', Validators.required],
      city: ['', Validators.required],
      homeAddress: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      occupation: ['', Validators.required],
      occupationInfo: ['', Validators.required],
      emailAddress: ['', Validators.compose([Validators.required, Validators.email])],
      // eslint-disable-next-line no-useless-escape
      password: ['', Validators.compose([Validators.required, Validators.minLength(8), Validators.pattern(this.passwordPattern)])],
      confirmPassword: ['', Validators.required]
    });
    this.registerForm.addValidators(this.confirmPasswordValidator);

    this.loginForm = this.fb.nonNullable.group({
      emailAddress: ['', Validators.compose([Validators.required, Validators.email])],
      // eslint-disable-next-line no-useless-escape
      password: ['', Validators.compose([Validators.required, Validators.minLength(8), Validators.pattern(this.passwordPattern)])]
    });
  }

  confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    return password?.value === confirmPassword?.value ? null : { mismatch: true };
  };


  onRegisterSubmit() {
    if (!this.registerForm.valid)
      return;

    // console.log(`Register form submitted with values: ${JSON.stringify(this.registerForm.}`));
    this.anonymousFacade.register(
      {
        userName: this.registerForm.controls.emailAddress.value ?? '',
        password: this.registerForm.controls.password.value ?? '',
        firstName: this.registerForm.controls.firstName.value,
        lastName: this.registerForm.controls.lastName.value,
        sex: this.registerForm.controls.sex.value,
        jmbg: this.registerForm.controls.jmbg.value,
        state: this.registerForm.controls.state.value,
        homeAddress: this.registerForm.controls.homeAddress.value,
        city: this.registerForm.controls.city.value,
        phoneNumber: this.registerForm.controls.phoneNumber.value,
        occupation: this.registerForm.controls.occupation.value,
        occupationInfo: this.registerForm.controls.occupationInfo.value,
      }
    );
  }

  fillRegisterWithValidData() {
    this.registerForm.controls.emailAddress.setValue("secretemail@email.org");
    this.registerForm.controls.password.setValue("Secret1234");
    this.registerForm.controls.confirmPassword.setValue("Secret1234");
    this.registerForm.controls.firstName.setValue("John",);
    this.registerForm.controls.lastName.setValue("Smith",);
    this.registerForm.controls.sex.setValue(0,);
    this.registerForm.controls.jmbg.setValue("1100888112233",);
    this.registerForm.controls.state.setValue("Serbia",);
    this.registerForm.controls.homeAddress.setValue("Ulica",);
    this.registerForm.controls.city.setValue("Novi Sad",);
    this.registerForm.controls.phoneNumber.setValue("0651234567",);
    this.registerForm.controls.occupation.setValue("Student",);
    this.registerForm.controls.occupationInfo.setValue("Finishing this project");
  }

}

