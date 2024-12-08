import {sharedEnvironment} from './environment.shared';

export const environment = {
  enableMockInterceptor: true,
  production: false,
  ...sharedEnvironment
};
