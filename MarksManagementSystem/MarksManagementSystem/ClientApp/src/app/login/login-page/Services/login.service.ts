import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'my-auth-token',
    'Access-Control-Allow-Credentials': 'true',
    'Access-Control-Allow-Origin': 'http://localhost:4200'
  })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  Url = 'api/account/login';
 // getStudentsUrl = 'https://localhost:44349/api/values/getstudents';
  constructor(private http: HttpClient, @Inject('BASE_URL') public baseUrl: string) { }

  public login(loginParams: any): Observable<any> {
    const me = this;
    return me.http.post<any>(this.baseUrl+this.Url, loginParams, httpOptions);
  }

 
}
