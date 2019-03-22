import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
export class SingupService {
  Url: string = "api/account/register";
  constructor(public http : HttpClient, @Inject('BASE_URL') public baseUrl: string) { }


  RegisterUser(signUpData):Observable<any>{
    return this.http.post(this.baseUrl+this.Url,signUpData, httpOptions);
  }
}
