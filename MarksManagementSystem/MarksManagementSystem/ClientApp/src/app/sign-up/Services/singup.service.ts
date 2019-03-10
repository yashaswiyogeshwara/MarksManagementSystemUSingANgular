import { Injectable } from '@angular/core';
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
  Url: string = "https://localhost:5001/api/account/register";
  constructor(public http : HttpClient) { }


  RegisterUser(signUpData):Observable<any>{
    return this.http.post(this.Url, signUpData, httpOptions);
  }
}
