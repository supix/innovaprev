import winston from 'winston';
import DailyRotateFile from 'winston-daily-rotate-file';
import path from 'path';
import fs from 'fs';
import { getConfig } from './config/get-config';

const logDir = path.join(__dirname, getConfig.logDirectory);
if (!fs.existsSync(logDir)) {
    fs.mkdirSync(logDir, { recursive: true });
}

const infoTransport = new DailyRotateFile({
    filename: path.join(logDir, 'access-%DATE%.log'),
    datePattern: 'YYYY-MM-DD',
    zippedArchive: true,
    maxFiles: '30d',
    level: 'info',
    handleExceptions: false,
    handleRejections: false
});

const errorTransport = new DailyRotateFile({
    filename: path.join(logDir, 'error-%DATE%.log'),
    datePattern: 'YYYY-MM-DD',
    zippedArchive: true,
    maxFiles: '60d',
    level: 'error',
    handleExceptions: false,
    handleRejections: false
});

const crashTransport = new DailyRotateFile({
    filename: path.join(logDir, 'crash-%DATE%.log'),
    datePattern: 'YYYY-MM-DD',
    zippedArchive: true,
    maxFiles: '90d',
    level: 'error',
    handleExceptions: true,
    handleRejections: true
});

export const logger = winston.createLogger({
    level: 'info',
    format: winston.format.combine(
        winston.format.timestamp(),
        winston.format.printf(({ timestamp, level, message }) => {
            return `${timestamp} [${level.toUpperCase()}]: ${message}`;
        })
    ),
    transports: [
        infoTransport,
        errorTransport,
        crashTransport,
        new winston.transports.Console({ level: 'debug' })
    ],
    exitOnError: false
});
