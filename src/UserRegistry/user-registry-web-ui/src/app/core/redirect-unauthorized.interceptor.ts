import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { catchError, Observable, throwError } from "rxjs";
import { IdentityApiService } from "../services/identity-api.service";

@Injectable()
export class RedirectUnauthorizedInterceptor implements HttpInterceptor {
    constructor(private router: Router, private identityService: IdentityApiService) {}
  
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error) => {
              
              if (error.status === 401) {
                this.identityService.logout();
                this.router.navigate(['/login']);
              }
              
              return throwError(error);
            })
          );
    }
}