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
import { ApplicationUserDto } from '../model/applicationUserDto';
import { CreateDonorCommand } from '../model/createDonorCommand';
import { CreatedDonorDto } from '../model/createdDonorDto';
import { Configuration } from '../configuration';

@Injectable()
export class IdentityService {
    
    public defaultHeaders = new HttpHeaders({
        'Accept': 'application/json', 
        'Content-Type': 'application/json'
    });

    constructor(protected httpClient: HttpClient, protected configuration: Configuration) {}

    /**
     * @param applicationUserId 
     * @param reportProgress flag to report request and response progress.
     */
    public apiIdentityApplicationUsersApplicationUserIdGet(applicationUserId: string, reportProgress?: boolean): Observable<HttpResponse<ApplicationUserDto>> {

        if (applicationUserId === null || applicationUserId === undefined) {
            throw new Error('Required parameter applicationUserId was null or undefined when calling apiIdentityApplicationUsersApplicationUserIdGet.');
        }

        return this.httpClient.request<ApplicationUserDto>('get', `${this.configuration.basePath}/api/Identity/ApplicationUsers/${encodeURIComponent(String(applicationUserId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: this.defaultHeaders,
                observe: 'response',
                reportProgress: reportProgress
            }
        );
    }

    /**
     * @param body
     * @param reportProgress flag to report request and response progress.
     */
    public apiIdentityDonorsPost(body?: CreateDonorCommand, reportProgress?: boolean): Observable<HttpResponse<CreatedDonorDto>> {

        return this.httpClient.request<CreatedDonorDto>('post', `${this.configuration.basePath}/api/Identity/Donors`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: this.defaultHeaders,
                observe: 'response',
                reportProgress: reportProgress
            }
        );
    }
}