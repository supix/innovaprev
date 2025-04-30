import morgan from 'morgan';
import { logger } from '../logger';
import { getConfig } from '../config/get-config';

const stream = {
    write: (message: string) => {
        const statusMatch = message.match(/" (\d{3}) /);
        const status = statusMatch ? parseInt(statusMatch[1], 10) : 200;

        if (status >= 500) {
            logger.error(message.trim());
        } else if (status >= 400) {
            logger.warn(message.trim());
        } else {
            logger.info(message.trim());
        }
    }
};

// Select format based on environment
const format = getConfig.nodeEnv === 'production' ? 'combined' : 'dev';

// Morgan middleware with custom stream
export const httpLogger = morgan(format, { stream });
