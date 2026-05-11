import { Injectable } from '@angular/core';
import {HttpClient, HttpResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  BillingPayload,
  CollectionsResponse,
  EnergyCalculationResult,
  EnergyCollectionsResponse,
  EnergyMunicipality,
  EnergyReportData,
  PricePayload,
  QuotationResponse,
  WindowsPayload
} from '../models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl: string = environment.api.baseUrl;
  private readonly endpoints = environment.api.endpoints;

  constructor(private http: HttpClient) {
  }

  // API to get the price
  getPrice(payload: PricePayload): Observable<QuotationResponse> {
    return this.http.post<QuotationResponse>(`${this.baseUrl}${this.endpoints.getQuote}`, payload);
  }

  // API to download the PDF
  downloadPdf(payload: BillingPayload): Observable<HttpResponse<Blob>> {
    return this.http.post(`${this.baseUrl}${this.endpoints.downloadPdf}`, payload, {responseType: 'blob', observe: 'response'});
  }

  // API to retrieve collections data
  getCollectionsData(): Observable<CollectionsResponse> {
    return this.http.get<CollectionsResponse>(`${this.baseUrl}${this.endpoints.collections}`);
  }

  getEnergyCollections(): Observable<EnergyCollectionsResponse> {
    return this.http.get<EnergyCollectionsResponse>(`${this.baseUrl}${this.endpoints.energyCollections}`);
  }

  searchEnergyMunicipalities(search: string, take: number = 30): Observable<EnergyMunicipality[]> {
    return this.http.get<EnergyMunicipality[]>(`${this.baseUrl}${this.endpoints.energyMunicipalities}`, {
      params: {
        search,
        take
      }
    });
  }

  calculateEnergyReport(payload: EnergyReportData & Partial<WindowsPayload>): Observable<EnergyCalculationResult> {
    return this.http.post<EnergyCalculationResult>(`${this.baseUrl}${this.endpoints.energyCalculate}`, payload);
  }

  getImageUrl(productCode: string, isThumb: boolean = true): string {
    return `${this.baseUrl}${this.endpoints.productImage}/${productCode}?isThumb=${isThumb}`;
  }

}
