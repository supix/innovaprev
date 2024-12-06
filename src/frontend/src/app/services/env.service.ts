import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EnvService {

  // Returns the configuration value or a default
  get apiBaseUrl(): string {
    return (window as any).env?.apiBaseUrl || 'https://api.example.com';
  }

  get anotherConfig(): string {
    return (window as any).env?.anotherConfig || 'default-value';
  }
}
