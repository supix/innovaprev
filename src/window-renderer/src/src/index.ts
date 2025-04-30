import './load-env';
import app from './server';
import { getConfig } from './config/get-config';
import { logger } from './logger';

/**
 * The port on which the Express server will run.
 */
const port = Number(getConfig.port);

/**
 * Starts the Express server and logs the status and Swagger documentation URL.
 */
const server = app.listen(port, () => {
    const baseUrl = `http://localhost:${port}${getConfig.swaggerUrl}`;
    logger.info(`Server started (${getConfig.nodeEnv}) on port ${port}`);
    if (getConfig.swagger && getConfig.swaggerUrl) {
        logger.info(`Swagger available at: ${baseUrl}`);
    }
});

/**
 * Global error handling for unhandled promise rejections.
 */
process.on('unhandledRejection', (reason) => {
    logger.error('Unhandled Rejection:', reason);
});

/**
 * Global error handling for uncaught exceptions.
 */
process.on('uncaughtException', (err) => {
    logger.error('Uncaught Exception:', err);
    // Optional: force shutdown
    // process.exit(1);
});

/**
 * Graceful shutdown on SIGINT (Ctrl+C) and SIGTERM (kill)
 */
const shutdown = (signal: string) => {
    logger.warn(`${signal} received. Shutting down...`);
    server.close(() => {
        logger.info('✅ Server closed gracefully.');
        process.exit(0);
    });
};

process.on('SIGINT', () => shutdown('SIGINT'));
process.on('SIGTERM', () => shutdown('SIGTERM'));

/**
 * Export the server instance for testing or external use.
 */
export default server;
