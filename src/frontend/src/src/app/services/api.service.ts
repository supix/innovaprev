import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {DataPayload, WindowEstimateResponse} from '../models';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl: string = '/api';

  constructor(private http: HttpClient) {
  }

  // API to get the price
  getPrice(payload: DataPayload): Observable<WindowEstimateResponse> {
    return this.http.post<WindowEstimateResponse>(`${this.baseUrl}/get-price`, payload);
  }

  // API to download the PDF
  downloadPdf(payload: DataPayload): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/download-pdf`, payload, {responseType: 'blob'});
  }

}
