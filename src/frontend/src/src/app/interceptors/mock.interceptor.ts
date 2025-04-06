import {HttpInterceptorFn, HttpResponse} from '@angular/common/http';
import {delay, of, throwError} from 'rxjs';
import {CollectionsResponse, Quotation, QuotationResponse} from '../models';
import {environment} from '../../environments/environment';

export const mockInterceptor: HttpInterceptorFn = (req, next) => {
  const baseUrl = environment.api.baseUrl;
  const endpoints = environment.api.endpoints;

  // Mock for /getQuote
  if (req.url.endsWith(`${baseUrl}${endpoints.getQuote}`) && req.method === 'POST') {
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

    const grandTotal = calculatePrice(body);
    const quotation: Quotation = {amount: grandTotal / 1.22, grandTotal, tax: grandTotal - (grandTotal / 1.22)};

    return of(new HttpResponse<QuotationResponse>({status: 200, body: {quotation}})).pipe(
      delay(1000)
    );
  }

  // Mock for /collections
  if (req.url === `${baseUrl}${endpoints.collections}` && req.method === 'GET') {
    return of(new HttpResponse<CollectionsResponse>({status: 200, body: getMockCollections()})).pipe(
      delay(1000)
    );
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

  if (!payload.productData) {
    errors.push('Missing productData object.');
  } else {
    const {
      product,
      internalColor,
      externalColor,
      accessoryColor
    } = payload.productData;
    if (!product) errors.push('productData.product is required.');
    if (!internalColor) errors.push('productData.internalColor is required.');
    if (!externalColor) errors.push('productData.externalColor is required.');
    if (!accessoryColor) errors.push('productData.accessoryColor is required.');
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

// Function to generate mock collections data
const getMockCollections = (): CollectionsResponse => ({
  product: [
    {id: 'PRO_GIALLO', desc: 'Giallo', trimSectionVisible: false, extDesc: 'Descrizione Giallo', singleColor: true, descTitle: ''},
    {id: 'PRO_VERDE', desc: 'Verde', trimSectionVisible: false, extDesc: 'Descrizione Verde', singleColor: true, descTitle: ''},
    {id: 'PRO_ROSSO', desc: 'Rosso', trimSectionVisible: false, extDesc: 'Descrizione Rosso', singleColor: false, descTitle: ''},
  ],
  colors: [
    {id: 'IC_GIALLO', desc: 'Giallo', internalColorForProduct: ['PRO_GIALLO'], externalColorForProduct: []},
    {id: 'IC_VERDE', desc: 'Verde', internalColorForProduct: ['PRO_VERDE'], externalColorForProduct: []},
    {id: 'IC_ROSSO', desc: 'Rosso', internalColorForProduct: [], externalColorForProduct: ['PRO_GIALLO', 'PRO_VERDE', 'PRO_ROSSO']},
  ],
  accessoryColors: [
    {id: 'AC_GIALLO', desc: 'Giallo'},
    {id: 'AC_VERDE', desc: 'Verde'},
    {id: 'AC_ROSSO', desc: 'Rosso'},
  ],
  windowTypes: [
    {id: 'WT_GRANDE', desc: 'Grande', numOfDims: 1, materialForProduct: ['PRO_GIALLO', 'PRO_VERDE', 'PRO_ROSSO'], glassTypeVisible: true, openingTypeVisible: true, wireCoverVisible: true},
    {id: 'WT_MEDIA', desc: 'Media', numOfDims: 2, materialForProduct: ['PRO_GIALLO', 'PRO_ROSSO'], glassTypeVisible: true, openingTypeVisible: false, wireCoverVisible: true},
    {id: 'WT_PICCOLA', desc: 'Piccola', numOfDims: 1, materialForProduct: ['PRO_VERDE', 'PRO_ROSSO'], glassTypeVisible: true, openingTypeVisible: true, wireCoverVisible: false},
  ],
  openingTypes: [
    {id: 'OT_DX', desc: 'SX'},
    {id: 'OT_SX', desc: 'DX'},
  ],
  glassTypes: [
    {id: 'GT_TRASPARENTE', desc: 'Trasparente'},
    {id: 'GT_OPACO', desc: 'Opaco'},
    {id: 'GT_AZZURRATO', desc: 'Azzurrato'},
  ],
  crosspieces: [
    {id: 'CRO_ALTA', desc: 'Alta'},
    {id: 'CRO_MEDIA', desc: 'Media'},
    {id: 'CRO_BASSA', desc: 'Bassa'},
  ],
});
