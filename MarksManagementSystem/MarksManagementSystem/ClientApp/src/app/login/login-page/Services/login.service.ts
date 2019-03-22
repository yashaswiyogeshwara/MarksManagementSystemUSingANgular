import { Injectable } from '@angular/core';
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

  Url = 'https://localhost:44349/api/account/login';
  getStudentsUrl = 'https://localhost:44349/api/values/getstudents';
  constructor(private http: HttpClient) { }

  public login(loginParams: any): Observable<any> {
    const me = this;
    return me.http.post<any>(me.Url , loginParams, httpOptions);
  }

  public getStudents():Observable<any>{
    debugger;
    const me = this;
    return me.http.get<any>(me.getStudentsUrl, httpOptions);
  }
}
