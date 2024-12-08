import {HttpInterceptorFn} from '@angular/common/http';
import {HttpErrorResponse} from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import {throwError} from 'rxjs';
import {inject} from '@angular/core';
import {ErrorModalService} from '../services/error-modal.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const errorModalService = inject(ErrorModalService);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status >= 400) {
        errorModalService.showErrorModal(error.message || 'An unknown error occurred!');
      }
      return throwError(() => error);
    })
  );
};
