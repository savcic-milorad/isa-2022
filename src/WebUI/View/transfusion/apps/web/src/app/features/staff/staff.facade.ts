import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { StaffService } from "../../shared/transfusion-api-client/api/staff.service";
import { StaffState } from "./state/staff.state";

@Injectable({
  providedIn: 'root'
})
export class StaffFacade {

  constructor(
    private staffApi: StaffService,
    private staffState: StaffState) { }

  isLoading$(): Observable<boolean> {
    return this.staffState.isLoading$();
  }

  hasSaidHello$(): Observable<boolean> {
    return this.staffState.hasSaidHello$();
  }

  sayHello() {
    this.staffState.setIsLoading(true);

    this.staffApi.apiStaffSayHelloGet()
    .pipe(
      catchError(this.handleError)
    )
    .subscribe(
    {
      next: () => this.staffState.setAlreadySaidHello(),
      error: () => this.staffState.setIsLoading(false),
      complete: () => this.staffState.setIsLoading(false)
    });
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 0) {
      return throwError(() => new Error(`A network or client error occurred due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.NotFound) {
      return throwError(() => new Error(`Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.BadRequest) {
      return throwError(() => new Error(`Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.InternalServerError) {
      return throwError(() => new Error('Server side error occured.'));
    }

    return throwError(() => new Error(`Error message with unknown code occured: ${error.error}`));
  }
}
