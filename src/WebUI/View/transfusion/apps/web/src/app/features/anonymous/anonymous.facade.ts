import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { CreateDonorCommand, LoginCommand } from "@transfusion/transfusion-api-client";
import { catchError, Observable, throwError } from "rxjs";
import { AuthState } from "../../shared/auth/auth.state.service";
import { IdentityService } from "../../shared/transfusion-api-client/api/identity.service";
import { AnonymousState } from "./state/anonymous.state";

@Injectable({
  providedIn: 'root'
})
export class AnonymousFacade {

  constructor(
    private identityApi: IdentityService,
    private anonymousState: AnonymousState,
    private authState: AuthState,
    private router: Router) { }

  isLoading$(): Observable<boolean> {
    return this.anonymousState.isLoading$();
  }

  register(command: CreateDonorCommand) {
    this.anonymousState.setIsLoading(true);

    this.identityApi.apiIdentityDonorsPost(command)
    .pipe(
      catchError(this.handleError)
    )
    .subscribe(
    {
      next: (val) => this.anonymousState.setCreatedDonor(val.body ?? {}),
      error: () => this.anonymousState.setIsLoading(false),
      complete: () => this.anonymousState.setIsLoading(false)
    });
  }

  login(command: LoginCommand) {
    this.anonymousState.setIsLoading(true);
    this.identityApi.apiIdentityLoginPost(command)
    .pipe(
      catchError(this.handleError)
    )
    .subscribe(
    {
      next: (val) => {
        this.authState.setAccessToken(val.body?.accessToken ?? '');

        const assignedRoleRoute = this.authState.getRouteForAssignedRole();
        console.log(`Assigned roule route: ${assignedRoleRoute}`);
        this.router.navigate([`/${assignedRoleRoute}`]);
      },
      error: () => this.anonymousState.setIsLoading(false),
      complete: () => this.anonymousState.setIsLoading(false)
    });
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 0) {
      return throwError(() => new Error(`A network or client error occurred due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.NotFound) {
      return throwError(() => new Error(`NotFound - Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.BadRequest) {
      return throwError(() => new Error(`BadRequest - Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.Forbidden) {
      return throwError(() => new Error(`Forbidden - Request could not be processed due to:\n${JSON.stringify(error.error)}`));
    }
    else if(error.status === HttpStatusCode.InternalServerError) {
      return throwError(() => new Error('InternalServerError - Server side error occured.'));
    }

    return throwError(() => new Error(`UNKNOWN - Error message with unknown code occured: ${error.error}`));
  }
}
