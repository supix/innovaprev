import { Injectable } from '@angular/core';

export interface SavedLogo {
  id: string;
  dataUrl: string; // base64 PNG/JPEG
  name?: string;
  createdAt: Date;
  updatedAt: Date;
  active?: boolean; // indicates selected logo
}

const STORAGE_KEY = 'innova_saved_logos';

@Injectable({ providedIn: 'root' })
export class LogoStorageService {
  private getAll(): SavedLogo[] {
    const raw = localStorage.getItem(STORAGE_KEY);
    return raw ? JSON.parse(raw, this.reviver) : [];
  }

  private saveAll(items: SavedLogo[]): void {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(items));
  }

  list(): SavedLogo[] {
    return this.getAll();
  }

  getById(id: string): SavedLogo | undefined {
    return this.getAll().find(x => x.id === id);
  }

  upsert(id: string | null, dataUrl: string, name?: string): string {
    const all = this.getAll();
    const now = new Date();

    if (id) {
      const index = all.findIndex(x => x.id === id);
      if (index !== -1) {
        all[index] = {
          ...all[index],
          dataUrl,
          name: name ?? all[index].name,
          updatedAt: now,
        };
        this.saveAll(all);
        return id;
      }
    }

    const newId = crypto.randomUUID();
    const isFirst = all.length === 0;
    const item: SavedLogo = {
      id: newId,
      dataUrl,
      name,
      createdAt: now,
      updatedAt: now,
      active: isFirst, // the only one is active by default
    };
    all.push(item);
    this.saveAll(all);
    return newId;
  }

  delete(id: string): boolean {
    const all = this.getAll();
    const removed = all.find(x => x.id === id);
    const next = all.filter(x => x.id !== id);
    const changed = next.length !== all.length;
    if (changed) {
      // if we removed the active one, ensure one remaining becomes active
      const hasActive = next.some(x => x.active);
      if (removed?.active && next.length > 0 && !hasActive) {
        next[0].active = true;
      }
      this.saveAll(next);
    }
    return changed;
  }

  clear(): void {
    localStorage.removeItem(STORAGE_KEY);
  }

  count(): number {
    return this.getAll().length;
  }

  setActive(id: string): void {
    const all = this.getAll();
    let changed = false;
    all.forEach(item => {
      const shouldBeActive = item.id === id;
      if ((item.active ?? false) !== shouldBeActive) {
        item.active = shouldBeActive;
        changed = true;
      }
    });
    if (changed) this.saveAll(all);
  }

  private reviver(key: string, value: any): any {
    if ((key === 'createdAt' || key === 'updatedAt') && typeof value === 'string') {
      return new Date(value);
    }
    return value;
  }
}
