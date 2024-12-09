import {HttpInterceptorFn} from '@angular/common/http';
import {HttpErrorResponse} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {throwError} from 'rxjs';
import {inject} from '@angular/core';
import {ErrorModalService} from '../services/error-modal.service';
import {environment} from "../../environments/environment";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const baseUrl = environment.api.baseUrl;
  const endpoints = environment.api.endpoints;

  const errorModalService = inject(ErrorModalService);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (req.url === `${baseUrl}${endpoints.collections}` && req.method === 'GET') {
        errorModalService.showErrorModal(error.message || 'An error occurred. Restart required.', true);
      } else if (error.status >= 400) {
        errorModalService.showErrorModal(error.message || 'An unknown error occurred!');
      }
      return throwError(() => error);
    })
  );
};
