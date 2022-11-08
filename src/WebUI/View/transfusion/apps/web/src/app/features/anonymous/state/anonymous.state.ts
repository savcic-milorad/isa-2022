import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { CenterSearchParameters } from "../models/center-search-parameters";

@Injectable()
export class AnonymousState {

  private updating$ = new BehaviorSubject<boolean>(false);
  private centerSearchParameters$ = new BehaviorSubject<CenterSearchParameters>(CenterSearchParameters.Initial());

  isUpdating$() {
    return this.updating$.asObservable();
  }

  setUpdating(isUpdating: boolean) {
    this.updating$.next(isUpdating);
  }

  getCenterSearchParameters$() {
    return this.centerSearchParameters$.asObservable();
  }

  setCenterSearchParameters(centerSearchParameters: CenterSearchParameters) {
    this.centerSearchParameters$.next(centerSearchParameters);
  }

  setCenterSearchParametersToInitial() {
    this.centerSearchParameters$.next(CenterSearchParameters.Initial());
  }
}
