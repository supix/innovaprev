import {sharedEnvironment} from './environment.shared';

export const environment = {
  enableMockInterceptor: false,
  production: true,
  enableFillFormButton: false,
  ...sharedEnvironment
};
