import {sharedEnvironment} from './environment.shared';

export const environment = {
  enableMockInterceptor: false,
  production: false,
  enableFillFormButton: true,
  ...sharedEnvironment
};
