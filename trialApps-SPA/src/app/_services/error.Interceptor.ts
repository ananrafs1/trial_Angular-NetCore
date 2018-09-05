import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { observable, throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(err => {
                if (err.status === 401){
                    return throwError(err.statusText);
                }

                if (err instanceof HttpErrorResponse) {
                    const applicationError = err.headers.get('Application-Error');
                    if (applicationError) {
                        console.error(applicationError);
                        return throwError(applicationError);
                    }
                    const serverError = err.error;
                    let modalStateErrors = '';
                    if (serverError && typeof serverError === 'object') {
                        for (const key in serverError) {
                            if (serverError[key]) {
                                modalStateErrors += serverError[key] + '\n';
                            }
                        }
                    }
                    return throwError(modalStateErrors || serverError || 'Server Errors');
                }
            })
        );
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
}