import { logger } from "../logger";
import { ValidateError } from "@tsoa/runtime";
import { ErrorRequestHandler } from "express";

export const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
    if (err instanceof ValidateError) {
        logger.warn(`[ValidateError] Validation failed on ${req.method} ${req.path} - ${JSON.stringify(err.fields)}`);
        res.status(422).json({
            message: 'Validation Failed',
            details: err.fields,
        });
        return;
    }

    if (err instanceof Error) {
        logger.error(`Error on ${req.method} ${req.path} - ${err.message}\n${err.stack}`);
        res.status(500).json({
            message: err.message || 'Something went wrong',
        });
        return;
    }

    const fallbackMessage = 'Unexpected error occurred';
    logger.error(`Unknown error on ${req.method} ${req.path} - ${JSON.stringify(err)}`);
    res.status(500).json({message: fallbackMessage});
};