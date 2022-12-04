/**
 *
 * OpenAPI spec version: v1
 *
 *
 * NOTE: This class was initially auto generated by the swagger code generator program,
 * and afterwards manually edited.
 */
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../configuration';

import { DonorPersonalInfoDto } from '@transfusion/transfusion-api-client';

@Injectable()
export class DonorService {

    public defaultHeaders = new HttpHeaders({
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    });

    constructor(protected httpClient: HttpClient, protected configuration: Configuration) {}

    /**
     * @param donorId
     * @param reportProgress flag to report request and response progress.
     */
    public apiDonorDonorIdGet(donorId: number): Observable<HttpResponse<DonorPersonalInfoDto>> {

        if (donorId === null || donorId === undefined) {
            throw new Error('Required parameter donorId was null or undefined when calling apiDonorDonorIdGet.');
        }

        return this.httpClient.request<DonorPersonalInfoDto>('get', `${this.configuration.basePath}/api/Donor/${encodeURIComponent(String(donorId))}`,
            {
                headers: this.defaultHeaders,
                observe: 'response'
            }
        );
    }
}
