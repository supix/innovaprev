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

  get formattedAmountIntegerPart(): string {
    return this.formatInteger(this.amount);
  }

  get formattedAmountDecimalPart(): string {
    return this.formatDecimal(this.amount);
  }

  get formattedTaxIntegerPart(): string {
    return this.formatInteger(this.tax);
  }

  get formattedTaxDecimalPart(): string {
    return this.formatDecimal(this.tax);
  }

  get isPriceNull(): boolean {
    return this.grandTotal == null || this.grandTotal === 0;
  }

  private formatInteger(value: number | null | undefined): string {
    if (value == null || value === 0) {
      return '0';
    }
    return Math.floor(value).toLocaleString('it-IT');
  }

  private formatDecimal(value: number | null | undefined): string {
    if (value == null || value === 0) {
      return '00';
    }
    return (value % 1).toFixed(2).substring(2);
  }
}
