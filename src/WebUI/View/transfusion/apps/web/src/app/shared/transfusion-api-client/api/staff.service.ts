
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders,HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../configuration';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  public defaultHeaders = new HttpHeaders({
    'Accept': 'application/json',
    'Content-Type': 'application/json'
  });

  constructor(protected httpClient: HttpClient, protected configuration: Configuration) { }

  /**
   */
  public apiStaffSayHelloGet(): Observable<HttpResponse<string>> {

    return this.httpClient.request<string>('get', `${this.configuration.basePath}/api/Staff/SayHello`,
      {
        headers: this.defaultHeaders,
        observe: 'response'
      }
    );
  }
}
