import { HttpClient, HttpParams, HttpResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core"; import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { resourceServerUrl } from '../../shared/app-constant';
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class AuthService {

    private loginURI: string = `${resourceServerUrl}auth/login`;
    private registerURI: string = `${resourceServerUrl}auth/register`;
    private isEmailRegisterURI: string = `${resourceServerUrl}auth/user-email-exist`;
    private jwtHelper = new JwtHelperService();
    private decodedToken: any;

    constructor(private http: HttpClient) { }

    login(model: any) {
        var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
        return this.http.post(this.loginURI, model, { headers: reqHeader }).pipe(
            map((response: any) => {                
                if (response) {                    
                    localStorage.setItem("id", response.id);
                    localStorage.setItem("token", response.token);
                    localStorage.setItem("username", response.name);                                        
                    this.decodedToken = this.jwtHelper.decodeToken(response.token);                    
                }
            })
        );
    }

    register(model: any): Observable<HttpResponse<any>> {
        var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
        return this.http.post(this.registerURI, model, {
            headers: new HttpHeaders({ 'No-Auth': 'True' })
                .set('Content-Type', 'application/json'), observe: 'response', responseType: 'text'
        });        
    }

    isExistEmail(email: string): Observable<HttpResponse<any>> {
        var reqHeader = new HttpHeaders({ 'No-Auth': 'True' });
        return this.http.get(this.isEmailRegisterURI, {
            headers: reqHeader, 
            params: new HttpParams()
                .set('email', email)
            , observe: 'response'
        });
    }

    loggedIn() {
        let token = localStorage.getItem("token")?.toString();
        return !this.jwtHelper.isTokenExpired(token);
    }    
}
