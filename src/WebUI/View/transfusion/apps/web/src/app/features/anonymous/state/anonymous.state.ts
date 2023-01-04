import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { CreatedDonorDto } from "@transfusion/transfusion-api-client";
import { CenterSearchParameters } from "../models/center-search-parameters";

@Injectable({
  providedIn: 'root'
})
export class AnonymousState {

  private loading$ = new BehaviorSubject<boolean>(false);
  private centerSearchParameters$ = new BehaviorSubject<CenterSearchParameters>(CenterSearchParameters.Initial());
  private createdDonor$ = new BehaviorSubject<CreatedDonorDto>({});

  isLoading$() {
    return this.loading$.asObservable();
  }

  setIsLoading(isLoading: boolean) {
    this.loading$.next(isLoading);
  }

  setCreatedDonor(createdDonor: CreatedDonorDto) {
    this.createdDonor$.next(createdDonor);
  }

  setCenterSearchParameters(centerSearchParameters: CenterSearchParameters) {
    this.centerSearchParameters$.next(centerSearchParameters);
  }

  setCenterSearchParametersToInitial() {
    this.centerSearchParameters$.next(CenterSearchParameters.Initial());
  }
}
