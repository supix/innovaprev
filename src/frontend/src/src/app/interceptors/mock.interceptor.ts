import {HttpInterceptorFn, HttpResponse} from '@angular/common/http';
import {delay, of, throwError} from 'rxjs';
import {
  CollectionsResponse,
  EnergyCalculationResult,
  EnergyCollectionsResponse,
  EnergyMunicipality,
  Quotation,
  QuotationResponse
} from '../models';
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

  if (req.url === `${baseUrl}${endpoints.energyCollections}` && req.method === 'GET') {
    return of(new HttpResponse<EnergyCollectionsResponse>({status: 200, body: getMockEnergyCollections()})).pipe(
      delay(500)
    );
  }

  if (req.url.startsWith(`${baseUrl}${endpoints.energyMunicipalities}`) && req.method === 'GET') {
    const search = (req.params.get('search') || '').toLowerCase();
    const body = getMockMunicipalities().filter(item =>
      !search || item.comune.toLowerCase().includes(search) || (item.provincia || '').toLowerCase().includes(search)
    );
    return of(new HttpResponse<EnergyMunicipality[]>({status: 200, body})).pipe(
      delay(300)
    );
  }

  if (req.url.endsWith(`${baseUrl}${endpoints.energyCalculate}`) && req.method === 'POST') {
    const body: any = req.body || {};
    const windowSurfaceSqm = Number(body.windowSurfaceSqm || 10);
    const investmentAmount = Number(body.investmentAmount || 10000);
    const oldWindowUw = Number(body.oldWindowUw || 2.8);
    const newWindowUw = Number(body.newWindowUw || 1.2);
    const deltaUw = Math.max(0, oldWindowUw - newWindowUw);
    const annualPrimaryEnergySavedKwh = Number((windowSurfaceSqm * deltaUw * 180).toFixed(2));
    const annualEconomicSaving = Number((annualPrimaryEnergySavedKwh * 0.11).toFixed(2));
    const annualDeductionQuota = Number((investmentAmount * 0.5 / 10).toFixed(2));
    const result: EnergyCalculationResult = {
      municipalityLabel: 'Torino (TO)',
      climateZone: 'E',
      degreeDays: 2617,
      fuelLabel: 'Metano',
      buildingTypeLabel: 'Residenziale',
      exposureTypeLabel: 'Verso l\'esterno',
      windowSurfaceSqm,
      investmentAmount,
      oldWindowUw,
      newWindowUw,
      deltaUw: Number(deltaUw.toFixed(3)),
      annualDispersionSavedKwh: Number((annualPrimaryEnergySavedKwh * 0.8).toFixed(2)),
      annualPrimaryEnergySavedKwh,
      annualEconomicSaving,
      annualCo2SavedKg: Number((annualPrimaryEnergySavedKwh * 0.216).toFixed(2)),
      deductionPercentage: 50,
      deductionTotal: Number((investmentAmount * 0.5).toFixed(2)),
      annualDeductionQuota,
      paybackYearsWithoutDeduction: annualEconomicSaving > 0 ? Number((investmentAmount / annualEconomicSaving).toFixed(1)) : null,
      paybackYearsWithDeduction: (annualEconomicSaving + annualDeductionQuota) > 0 ? Number((investmentAmount / (annualEconomicSaving + annualDeductionQuota)).toFixed(1)) : null
    };

    return of(new HttpResponse<EnergyCalculationResult>({status: 200, body: result})).pipe(
      delay(800)
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
    {id: 'PRO_GIALLO', desc: 'Giallo', extDesc: 'Descrizione Giallo', singleColor: true, descTitle: ''},
    {id: 'PRO_VERDE', desc: 'Verde', extDesc: 'Descrizione Verde', singleColor: true, descTitle: ''},
    {id: 'PRO_ROSSO', desc: 'Rosso', extDesc: 'Descrizione Rosso', singleColor: false, descTitle: ''},
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
    {id: 'WT_GRANDE', desc: 'Grande', numOfDims: 1, materialForProduct: ['PRO_GIALLO', 'PRO_VERDE', 'PRO_ROSSO'], glassTypeVisible: true, openingTypeVisible: true, wireCoverVisible: true, frameTypeVisible: true},
    {id: 'WT_MEDIA', desc: 'Media', numOfDims: 2, materialForProduct: ['PRO_GIALLO', 'PRO_ROSSO'], glassTypeVisible: true, openingTypeVisible: false, wireCoverVisible: true, frameTypeVisible: true},
    {id: 'WT_PICCOLA', desc: 'Piccola', numOfDims: 1, materialForProduct: ['PRO_VERDE', 'PRO_ROSSO'], glassTypeVisible: true, openingTypeVisible: true, wireCoverVisible: false, frameTypeVisible: false},
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
  frameTypes: [
    {id: 'A_FRAME', desc: 'Frame A', frameForProduct: []}
  ]
});

const getMockEnergyCollections = (): EnergyCollectionsResponse => ({
  fuels: [
    {id: '4', label: 'Metano', pci: 8.7, pricePerUnit: 0.89, unit: '(Kwh/Nm³)', priceLabel: '€ Kwh/Nm³'},
    {id: '1', label: 'Energia Elettrica', pci: 1, pricePerUnit: 0.2, unit: '(Kwh/Kw)', priceLabel: '€/Kwh/Kw'},
  ],
  deductions: [
    {id: 'ecobonus_50pct', label: 'Ecobonus 50%', percentage: 50, maxExpense: 96000, isApplicable: true},
    {id: 'bonus_casa_50pct', label: 'Bonus casa 50%', percentage: 50, maxExpense: 96000, isApplicable: true},
    {id: 'nuova_costruzione', label: 'Nuova costruzione', percentage: 0, maxExpense: 0, isApplicable: false}
  ],
  buildingTypes: [
    {id: 'residenziale', label: 'Residenziale', factor: 0.9},
    {id: 'non_residenziale', label: 'Non residenziale', factor: 0.6}
  ],
  exposureTypes: [
    {id: 'verso_esterno', label: 'Verso l\'esterno', factor: 1},
    {id: 'verso_ambiente_non_riscaldato', label: 'Verso ambiente non riscaldato', factor: 0.5},
    {id: 'su_terreno', label: 'Su terreno', factor: 0.8}
  ],
  oldFrameTypes: [
    {id: 'legno_duro', label: 'Legno duro spessore 50', uw: 2.4},
    {id: 'pvc_due_camere', label: 'PVC a due camere', uw: 2.2}
  ],
  oldGlassTypes: [
    {id: 'vetrata_singola', label: 'Vetrata singola', uw: 5.7},
    {id: 'vetrata_4_12_4', label: 'Vetrata 4-12-4', uw: 2.9}
  ],
  frameAreaRatios: [
    {id: '0.2', label: '20%', ratio: 0.2},
    {id: '0.3', label: '30%', ratio: 0.3}
  ],
  permeabilityClasses: [],
  shadingOptions: []
});

const getMockMunicipalities = (): EnergyMunicipality[] => ([
  {
    id: 'torino__to',
    comune: 'Torino',
    cap: '10121',
    provincia: 'TO',
    regione: 'Piemonte',
    altitudineSlm: 239,
    gradiGiorno: 2617,
    zonaClimatica: 'E',
    zonaVento: '1'
  },
  {
    id: 'milano__mi',
    comune: 'Milano',
    cap: '20121',
    provincia: 'MI',
    regione: 'Lombardia',
    altitudineSlm: 120,
    gradiGiorno: 2404,
    zonaClimatica: 'E',
    zonaVento: '1'
  }
]);
