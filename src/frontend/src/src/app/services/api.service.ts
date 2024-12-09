import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CollectionsResponse, DataPayload, QuotationResponse} from '../models';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl: string = environment.api.baseUrl;
  private readonly endpoints = environment.api.endpoints;

  constructor(private http: HttpClient) {
  }

  // API to get the price
  getPrice(payload: DataPayload): Observable<QuotationResponse> {
    return this.http.post<QuotationResponse>(`${this.baseUrl}${this.endpoints.getQuote}`, payload);
  }

  // API to download the PDF
  downloadPdf(payload: DataPayload): Observable<Blob> {
    return this.http.post(`${this.baseUrl}${this.endpoints.downloadPdf}`, payload, {responseType: 'blob'});
  }

  // API to retrieve collections data
  getCollectionsData(): Observable<CollectionsResponse> {
    return this.http.get<CollectionsResponse>(`${this.baseUrl}${this.endpoints.collections}`);
  }

}
