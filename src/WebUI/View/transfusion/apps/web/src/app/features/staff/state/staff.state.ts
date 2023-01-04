import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class StaffState {

  private loading$ = new BehaviorSubject<boolean>(false);
  private alreadySaidHello$ = new BehaviorSubject<boolean>(false);

  isLoading$() {
    return this.loading$.asObservable();
  }

  hasSaidHello$() {
    return this.alreadySaidHello$.asObservable();
  }

  setIsLoading(isLoading: boolean) {
    this.loading$.next(isLoading);
  }

  setAlreadySaidHello() {
    this.alreadySaidHello$.next(true);
  }
}
