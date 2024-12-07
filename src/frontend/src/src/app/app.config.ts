import {ApplicationConfig} from '@angular/core';
import {provideRouter} from '@angular/router';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {routes} from './app.routes';
import {mockInterceptor} from './mock.interceptor';
import {environment} from '../environments/environments';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      ...(environment.enableMockInterceptor ? [withInterceptors([mockInterceptor])] : [])
    )
  ]
};
