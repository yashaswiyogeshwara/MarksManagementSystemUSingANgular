import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  authorizationToken: string;
  constructor() { }

  public getAuthorizationToken() {
    debugger;
    return this.authorizationToken || window.localStorage.getItem("mmsAuthToken");
    }

  public setAuthorizationToken(value : string) {
    debugger;
    this.authorizationToken = value;
    window.localStorage.clear();
    window.localStorage.setItem('mmsAuthToken',value);
  }
}
