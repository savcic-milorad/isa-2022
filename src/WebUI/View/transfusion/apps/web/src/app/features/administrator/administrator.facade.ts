import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { AdministratorService } from "../../shared/transfusion-api-client/api/administrator.service";
import { AdministratorState } from "./state/administator.state";

@Injectable({
  providedIn: 'root'
})
export class AdministratorFacade {

  constructor(
    private administratorApi: AdministratorService,
    private administratorState: AdministratorState) { }

  isLoading$(): Observable<boolean> {
    return this.administratorState.isLoading$();
  }

  hasSaidHello$(): Observable<boolean> {
    return this.administratorState.hasSaidHello$();
  }

  sayHello() {
    this.administratorState.setIsLoading(true);

    this.administratorApi.apiAdministratorSayHelloGet()
      .pipe(
        catchError(this.handleError)
      )
      .subscribe(
        {
          next: () => this.administratorState.setSaidHello(true),
          error: () => this.administratorState.setSaidHello(false),
          complete: () => this.administratorState.setIsLoading(false)
        });
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 0) {
      return throwError(() => new Error(`A network or client error occurred due to:\n${JSON.stringify(error.error)}`));
    }
    else if (error.status === HttpStatusCode.NotFound) {
      return throwError(() => new Error(`Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if (error.status === HttpStatusCode.BadRequest) {
      return throwError(() => new Error(`Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if (error.status === HttpStatusCode.InternalServerError) {
      return throwError(() => new Error('Server side error occured.'));
    }

    return throwError(() => new Error(`Error message with unknown code occured: ${error.error}`));
  }
}
