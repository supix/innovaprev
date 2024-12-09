import {Component, Input} from '@angular/core';
import {CommonModule} from "@angular/common";

@Component({
  standalone: true,
  selector: 'app-price-display',
  templateUrl: './price-display.component.html',
  imports: [CommonModule],
  styleUrls: ['./price-display.component.scss']
})
export class PriceDisplayComponent {
  @Input() price: number | null | undefined = null;

  get formattedIntegerPart(): string {
    if (this.price == null || this.price === 0) {
      return '0';
    }
    return Math.floor(this.price).toLocaleString('it-IT');
  }

  get formattedDecimalPart(): string {
    if (this.price == null || this.price === 0) {
      return '00';
    }
    return (this.price % 1).toFixed(2).substring(2);
  }

  get isPriceNull(): boolean {
    return this.price == null || this.price === 0;
  }
}
