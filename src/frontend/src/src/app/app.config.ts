import { ApplicationConfig, LOCALE_ID } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { routes } from './app.routes';
import { mockInterceptor } from './interceptors/mock.interceptor';
import { environment } from '../environments/environment';
import { errorInterceptor } from './interceptors/errors.interceptors';
import { registerLocaleData } from '@angular/common';
import localeIt from '@angular/common/locales/it';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';

registerLocaleData(localeIt);

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideToastr({
      autoDismiss: true,
      maxOpened: 1
    }),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        ...(environment.enableMockInterceptor ? [mockInterceptor] : []),
        errorInterceptor
      ])
    ),
    {provide: LOCALE_ID, useValue: 'it'}
  ]
};
