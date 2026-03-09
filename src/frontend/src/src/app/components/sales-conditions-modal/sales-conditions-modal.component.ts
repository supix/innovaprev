import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sales-conditions-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sales-conditions-modal.component.html',
  styleUrl: './sales-conditions-modal.component.scss',
})
export class SalesConditionsModalComponent {
  readonly maxConditions = 20;

  constructor(private readonly cdr: ChangeDetectorRef) {}

  private _initialValue: string[] = [];
  @Input() set initialValue(value: string[]) {
    this._initialValue = value ?? [];
    this.conditions = this._initialValue.length ? [...this._initialValue] : [''];
    this.cdr.detectChanges();
  }

  get initialValue(): string[] {
    return this._initialValue;
  }

  conditions: string[] = [''];

  confirmCallback?: (value: string[]) => void;

  get nextRowNumber(): number {
    return Math.min(this.conditions.length + 1, this.maxConditions);
  }

  get canAddCondition(): boolean {
    if (this.conditions.length >= this.maxConditions) {
      return false;
    }

    return this.conditions.length === 0 || this.conditions[this.conditions.length - 1].trim().length > 0;
  }

  addCondition(value: string = ''): void {
    if (!this.canAddCondition) {
      return;
    }
    this.conditions = [...this.conditions, value];
    this.cdr.detectChanges();
  }

  removeCondition(index: number): void {
    this.conditions = this.conditions.filter((_, currentIndex) => currentIndex !== index);
    if (this.conditions.length === 0) {
      this.addCondition();
      return;
    }
    this.cdr.detectChanges();
  }

  updateCondition(index: number, value: string): void {
    this.conditions = this.conditions.map((condition, currentIndex) =>
      currentIndex === index ? value : condition
    );
    this.cdr.detectChanges();
  }

  trackByIndex(index: number): number {
    return index;
  }

  close(): void {
    document.body.removeChild(document.getElementById('app-sales-conditions-modal')!);
  }

  confirm(): void {
    const value = this.conditions.map(item => item.trim()).filter(Boolean);
    this.confirmCallback?.(value);
  }
}
