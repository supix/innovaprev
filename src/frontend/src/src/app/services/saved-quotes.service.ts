import { Injectable } from '@angular/core';
import { BillingPayload, Quotation } from '../models';

export interface SavedQuote {
  id: string;
  billingPayload: BillingPayload;
  quotation: Quotation;
  createdAt: Date;
  updatedAt: Date;
}

const STORAGE_KEY = 'saved_quotes';

@Injectable({
  providedIn: 'root'
})
export class SavedQuotesService {

  constructor() {}

  private getAll(): SavedQuote[] {
    const raw = localStorage.getItem(STORAGE_KEY);
    return raw ? JSON.parse(raw, this.reviver) : [];
  }

  private saveAll(quotes: SavedQuote[]): void {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(quotes));
  }

  list(): SavedQuote[] {
    return this.getAll();
  }

  getById(id: string): SavedQuote | undefined {
    return this.getAll().find(q => q.id === id);
  }

  upsert(id: string | null, billingPayload: BillingPayload, quotation: Quotation): string {
    const all = this.getAll();
    const now = new Date();

    if (id) {
      const index = all.findIndex(q => q.id === id);
      if (index !== -1) {
        all[index] = {
          id,
          billingPayload,
          quotation,
          createdAt: new Date(all[index].createdAt),
          updatedAt: now
        };
        this.saveAll(all);
        return id;
      }
    }

    const newId = crypto.randomUUID();
    const newQuote: SavedQuote = {
      id: newId,
      billingPayload,
      quotation,
      createdAt: now,
      updatedAt: now
    };
    all.push(newQuote);
    this.saveAll(all);
    return newId;
  }

  delete(id: string): boolean {
    const all = this.getAll();
    const newAll = all.filter(q => q.id !== id);
    const changed = newAll.length !== all.length;
    if (changed) this.saveAll(newAll);
    return changed;
  }

  count(): number {
    return this.list().length;
  }

  clear(): void {
    localStorage.removeItem(STORAGE_KEY);
  }

  private reviver(key: string, value: any): any {
    if ((key === 'createdAt' || key === 'updatedAt') && typeof value === 'string') {
      return new Date(value);
    }
    return value;
  }
}
