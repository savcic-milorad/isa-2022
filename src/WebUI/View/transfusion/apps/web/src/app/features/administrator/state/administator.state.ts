import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AdministratorState {

  private loading$ = new BehaviorSubject<boolean>(false);
  private alreadySaidHello$ = new BehaviorSubject<boolean>(false);

  isLoading$() {
    return this.loading$.asObservable();
  }

  setIsLoading(isLoading: boolean) {
    this.loading$.next(isLoading);
  }

  hasSaidHello$() {
    return this.alreadySaidHello$.asObservable();
  }

  setSaidHello(hasSaidHello: boolean) {
    this.alreadySaidHello$.next(hasSaidHello);
  }
}
