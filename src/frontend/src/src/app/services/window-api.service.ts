import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { WindowRow } from '../models';

@Injectable({
  providedIn: 'root',
})
export class WindowApiService {
  private readonly baseUrl: string = environment.api.windowRendererBaseUrl;
  private readonly endpoints = environment.api.windowRendererEndpoints;

  constructor(private http: HttpClient) {}

  /**
   * Calls the drawWindow endpoint and returns the PNG as a Blob
   *
   * @param input the window parameters (height, width, materialType, etc.)
   */
  drawWindow(input: WindowRow): Observable<Blob> {
    const query = new URLSearchParams();

    query.set('height', input.height.toString());
    query.set('width', input.width.toString());
    query.set('materialType', input.windowType);

    if (input.wireCover !== undefined) query.set('wireCover', input.wireCover.toString());
    if (input.glassType) query.set('glassType', input.glassType);
    if (input.openingType) query.set('openingType', input.openingType);

    const url = `${this.baseUrl}${this.endpoints.windows}/drawWindow?${query.toString()}`;
    return this.http.get(url, { responseType: 'blob' });
  }
}
