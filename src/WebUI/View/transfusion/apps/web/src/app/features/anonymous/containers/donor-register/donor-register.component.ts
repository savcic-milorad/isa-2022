import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Sex } from 'apps/web/src/app/shared/transfusion-api-client/model/sex';

@Component({
  selector: 'transfusion-donor-register',
  templateUrl: './donor-register.component.html',
  styleUrls: ['./donor-register.component.scss'],
})
export class DonorRegisterComponent {

  registerForm = new FormGroup({
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      sex: new FormControl<Sex>(Sex.Female),
      jmbg: new FormControl(''),
      state: new FormControl(''),
      city: new FormControl(''),
      homeAddress: new FormControl(''),
      phoneNumber: new FormControl(''),
      occupation: new FormControl(''),
      occupationInfo: new FormControl(''),
      emailAddress: new FormControl(''),
      password: new FormControl(''),
      confirmPassword: new FormControl('')
  });
}
