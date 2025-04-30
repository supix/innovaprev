import {getConfigConverter} from "../helpers/get-config.converter";

export interface IGetConfig {
  nodeEnv: string;
  swagger: boolean;
  swaggerUrl: string;
  port: number;
  logDirectory: string;
}

// default configuration
export const defaultConfig: IGetConfig = {
  nodeEnv: 'development',
  swagger: false,
  swaggerUrl: '/api-docs',
  port: 5000,
  logDirectory: 'logs',
}

export const getConfig: IGetConfig = getConfigConverter(defaultConfig);
