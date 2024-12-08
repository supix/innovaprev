import {HttpInterceptorFn} from '@angular/common/http';
import {HttpResponse} from '@angular/common/http';
import {of, throwError} from 'rxjs';
import {WindowEstimateResponse} from '../models';

export const mockInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.endsWith('/api/get-price') && req.method === 'POST') {
    const body = req.body;
    const validationErrors = validatePayload(body);

    if (validationErrors.length > 0) {
      return throwError(() => ({
        status: 400,
        statusText: 'Bad Request',
        error: {
          message: 'Invalid payload',
          details: validationErrors
        }
      }));
    }

    const totalEstimatedPrice = calculatePrice(body);

    const mockEstimation: WindowEstimateResponse = {
      totalEstimatedPrice
    };

    return of(new HttpResponse({status: 200, body: mockEstimation}));
  }
  return next(req);
};

function calculatePrice(payload: any): number {
  const windowsData = payload.windowsData;
  const totalQuantity = windowsData.reduce((sum: number, window: any) => sum + window.quantity, 0);
  return 499.9887654 + totalQuantity * 400;
}

function validatePayload(payload: any): string[] {
  const errors: string[] = [];

  if (!payload.personalData) {
    errors.push('Missing personalData object.');
  } else {
    const {companyName, address, vat, phone, mail, orderNumber} = payload.personalData;
    if (!companyName) errors.push('personalData.companyName is required.');
    if (!address) errors.push('personalData.address is required.');
    if (!vat) errors.push('personalData.vat is required.');
    if (!phone) errors.push('personalData.phone is required.');
    if (!mail) errors.push('personalData.mail is required.');
    if (!orderNumber) errors.push('personalData.orderNumber is required.');
  }

  if (!payload.productData) {
    errors.push('Missing productData object.');
  } else {
    const {
      product,
      glassStopper,
      windowSlide,
      internalColor,
      externalColor,
      accessoryColor,
      climateZone
    } = payload.productData;
    if (!product) errors.push('productData.product is required.');
    if (glassStopper === undefined) errors.push('productData.glassStopper is required.');
    if (windowSlide === undefined) errors.push('productData.windowSlide is required.');
    if (!internalColor) errors.push('productData.internalColor is required.');
    if (!externalColor) errors.push('productData.externalColor is required.');
    if (!accessoryColor) errors.push('productData.accessoryColor is required.');
    if (!climateZone) errors.push('productData.climateZone is required.');
  }

  if (!payload.windowsData || !Array.isArray(payload.windowsData) || payload.windowsData.length === 0) {
    errors.push('windowsData must be a non-empty array.');
  } else {
    payload.windowsData.forEach((window: any, index: number) => {
      if (!window.position) errors.push(`windowsData[${index}].position is required.`);
      if (!window.height) errors.push(`windowsData[${index}].height is required.`);
      if (!window.width) errors.push(`windowsData[${index}].width is required.`);
      if (window.quantity === undefined) errors.push(`windowsData[${index}].quantity is required.`);
    });
  }

  return errors;
}
