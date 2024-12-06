import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {EnvService} from './env.service';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private readonly baseUrl: string;

  constructor(private http: HttpClient, envService: EnvService) {
    this.baseUrl = envService.apiBaseUrl;
  }

  // API to get the price
  getPrice(payload: any): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/get-price`, payload);
  }

  // API to download the PDF
  downloadPdf(payload: any): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/download-pdf`, payload, {responseType: 'blob'});
  }
}
