import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BillingPayload, CollectionsResponse, PricePayload, QuotationResponse } from '../models';
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
  downloadPdf(payload: BillingPayload): Observable<Blob> {
    return this.http.post(`${this.baseUrl}${this.endpoints.downloadPdf}`, payload, {responseType: 'blob'});
  }

  // API to retrieve collections data
  getCollectionsData(): Observable<CollectionsResponse> {
    return this.http.get<CollectionsResponse>(`${this.baseUrl}${this.endpoints.collections}`);
  }

  getImageUrl(productCode: string, isThumb: boolean = true): string {
    return `${this.baseUrl}${this.endpoints.productImage}/${productCode}?isThumb=${isThumb}`;
  }

}
