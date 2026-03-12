import { Component, Input } from '@angular/core';
import { CommonModule } from "@angular/common";

@Component({
  standalone: true,
  selector: 'app-price-display',
  templateUrl: './price-display.component.html',
  imports: [CommonModule],
  styleUrls: ['./price-display.component.scss']
})
export class PriceDisplayComponent {
  @Input() grandTotal: number | null | undefined = null;
  @Input() amount: number | null | undefined = null;
  @Input() tax: number | null | undefined = null;

  private readonly currencyFormatter = new Intl.NumberFormat('it-IT', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
    useGrouping: true
  });

  get formattedAmountIntegerPart(): string {
    return this.formatParts(this.amount).integer;
  }

  get formattedAmountDecimalPart(): string {
    return this.formatParts(this.amount).decimal;
  }

  get formattedTaxIntegerPart(): string {
    return this.formatParts(this.tax).integer;
  }

  get formattedTaxDecimalPart(): string {
    return this.formatParts(this.tax).decimal;
  }

  get isPriceNull(): boolean {
    return this.grandTotal == null || this.grandTotal === 0;
  }

  private formatParts(value: number | null | undefined): { integer: string; decimal: string } {
    const safeValue = typeof value === 'number' && Number.isFinite(value) ? value : 0;
    const parts = this.currencyFormatter.formatToParts(safeValue);

    return {
      integer: parts
        .filter((part) => part.type === 'integer' || part.type === 'group')
        .map((part) => part.value)
        .join('') || '0',
      decimal: parts.find((part) => part.type === 'fraction')?.value || '00'
    };
  }
}
