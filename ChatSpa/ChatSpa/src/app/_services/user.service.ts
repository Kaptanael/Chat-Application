import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpParams, HttpHeaders, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { resourceServerUrl } from '../shared/app-constant';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private allUserURI: string = `${resourceServerUrl}users/get-all-user`;    
    private allUserExceptByIdURI: string = `${resourceServerUrl}users/get-all-user-except-by-id`;
    private userById: string = `${resourceServerUrl}users/get-user-by-id/`;

    constructor(private http: HttpClient) {
    }

    getAllUser(): Observable<HttpResponse<any>> {
        return this.http.get(this.allUserURI, { observe: 'response' });
    }   

    getAllUserExceptById(): Observable<HttpResponse<any>> {
        return this.http.get(this.allUserExceptByIdURI, { observe: 'response' });
    }

    getUserById(id: string): Observable<HttpResponse<any>> {
        return this.http.get(this.userById + id, { observe: 'response' });
    }   
}
