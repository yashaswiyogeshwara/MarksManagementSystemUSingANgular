import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  authorizationToken: string;
  constructor() { }

  public getAuthorizationToken() {
    return this.authorizationToken;
  }

  public setAuthorizationToken(value : string) {
    this.authorizationToken = value;
  }
}
