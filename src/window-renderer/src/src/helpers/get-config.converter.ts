import { IGetConfig } from "../config/get-config";

/**
 * takes all config object values from the process.ENV otherwise it uses the default.
 * also converts interface from camelcase to snake case as process.ENV standard
 * ex.: transform "uploadsLimit.fileSizeMb" -> "UPLOADS_LIMIT_FILE_SIZE_MB"
 */
export function getConfigConverter(config: any, obj: any = {}, prefix: string = ''): IGetConfig {
  for (let configKey in config) {
    const type = typeof config[configKey];
    if (type === 'object' && !Array.isArray(config[configKey])) {
      obj[configKey] = getConfigConverter(config[configKey], {}, `${prefix}${camelToSnake(configKey)}_`);
    } else {
      const snakeKey = `${prefix}${camelToSnake(configKey)}`;
      const value = process.env[snakeKey] || config[configKey as keyof typeof config]
      obj[configKey] = convertToBoolean(value);
    }
  }
  return obj as IGetConfig;
}

function camelToSnake(str: string) {
  if (/^[a-z]+(?:[A-Z][a-z]+)*$/.test(str)) {
    return str.replace(/[A-Z]/g, letter => `_${letter.toLowerCase()}`).toUpperCase();
  } else {
    return str;
  }
}

function convertToBoolean(value: string | boolean | any): string | boolean | any {
  if (typeof value === "boolean") {
    return value;
  }

  if (typeof value === "string") {

    if (value.toLowerCase() === "true" || value.toLowerCase() === "yes" || value.toLowerCase() === "enable") {
      return true;
    }

    if (value === "false" || value === "no" || value === "disable") {
      return false;
    }
  }

  return value;
}
