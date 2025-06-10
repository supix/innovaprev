import { HttpInterceptorFn } from '@angular/common/http';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { inject } from '@angular/core';
import { ModalService } from '../services/modal.service';
import { environment } from "../../environments/environment";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const baseUrl = environment.api.baseUrl;
  const endpoints = environment.api.endpoints;

  const modalService = inject(ModalService);
  if (req.url.endsWith('/drawableWindow') && req.method === 'GET') {
    return next(req);
  }
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (req.url === `${baseUrl}${endpoints.collections}` && req.method === 'GET') {
        modalService.showErrorModal(error.message || 'An error occurred. Restart required.', true);
      } else if (error.status >= 400) {
        modalService.showErrorModal(error.message || 'An unknown error occurred!');
      }
      return throwError(() => error);
    })
  );
};
