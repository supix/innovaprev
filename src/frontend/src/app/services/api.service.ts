import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'https://api.example.com'; // Replace with actual API base URL

  constructor(private http: HttpClient) {}

  // API to get the price
  getPrice(payload: any): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/get-price`, payload);
  }

  // API to download the PDF
  downloadPdf(payload: any): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/download-pdf`, payload, { responseType: 'blob' });
  }
}
