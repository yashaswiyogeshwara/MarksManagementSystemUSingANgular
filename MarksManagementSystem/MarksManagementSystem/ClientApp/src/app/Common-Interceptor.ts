import { HttpInterceptor, HttpRequest, HttpHandler } from "@angular/common/http";
import { RequestService } from "./Request.service";
import { Injectable } from "@angular/core";

@Injectable()
 export class CommonInterceptor implements HttpInterceptor{
  constructor(public requestService: RequestService) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler) : any{
    let me = this;
    debugger;
    const authToken = this.requestService.getAuthorizationToken() || "unauth";

    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    const authReq = req.clone({
      headers: req.headers.set('Authorization', authToken)
    });

    // send cloned request with header to the next handler.
    return next.handle(authReq);
    }
}
