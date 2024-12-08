import {ApplicationConfig} from '@angular/core';
import {provideRouter} from '@angular/router';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {routes} from './app.routes';
import {mockInterceptor} from './interceptors/mock.interceptor';
import {environment} from '../environments/environment';
import {errorInterceptor} from './interceptors/errors.interceptors';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        ...(environment.enableMockInterceptor ? [mockInterceptor] : []),
        errorInterceptor
      ])
    )
  ]
};
