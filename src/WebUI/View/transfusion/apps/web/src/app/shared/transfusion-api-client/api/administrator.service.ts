
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders,HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../configuration';



@Injectable({
  providedIn: 'root'
})
export class AdministratorService {

  public defaultHeaders = new HttpHeaders({
    'Accept': 'application/json',
    'Content-Type': 'application/json'
  });

  constructor(protected httpClient: HttpClient, protected configuration: Configuration) { }

  /**
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public apiAdministratorSayHelloGet(): Observable<HttpResponse<string>> {

    return this.httpClient.request<string>('get', `${this.configuration.basePath}/api/Administrator/SayHello`,
      {
        headers: this.defaultHeaders,
        observe: 'response'
      }
    );
  }

}
