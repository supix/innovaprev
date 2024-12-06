import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {ApiService} from '../services/api.service';
import {BillingDataPayload, DataPayload, PersonalDataPayload, WindowEstimateRowPayload} from '../models';

@Component({
  selector: 'app-prev-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './prev-form.component.html',
  styleUrls: ['./prev-form.component.scss'],
})
export class PrevFormComponent {
  // Personal data model
  personalData: PersonalDataPayload = {
    firstName: '',
    lastName: '',
    address: ''
  };

  // Billing data model
  billingData: BillingDataPayload = {
    vatNumber: '',
    taxCode: '',
    billingAddress: ''
  };

  // Window estimate data model
  rows: WindowEstimateRowPayload[] = [
    {height: 100, width: 200, color: 'White', quantity: 1}
  ];

  // Calculated price
  price: number | null = null;

  constructor(private apiService: ApiService) {
  }

  // Action: Calculate the price
  calculatePrice(): void {
    const payload = this.buildPayload();
    this.apiService.getPrice(payload).subscribe(({totalEstimatedPrice}) => {
      this.price = totalEstimatedPrice;
    });
  }

  // Action: Download the PDF
  downloadPdf(): void {
    const payload = this.buildPayload();
    this.apiService.downloadPdf(payload).subscribe((response) => {
      const blob = new Blob([response], {type: 'application/pdf'});
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      const timestamp = Date.now();
      a.href = url;
      a.download = `estimate-${timestamp}.pdf`;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }

  // Build payload for API
  private buildPayload(): DataPayload {
    return {
      personalData: this.personalData,
      billingData: this.billingData,
      windows: this.rows
    };
  }

  // Add a new row to the window estimate table
  addRow(): void {
    this.rows.push({height: 0, width: 0, color: '', quantity: 0});
  }

  // Remove a specific row from the window estimate table
  removeRow(index: number): void {
    this.rows.splice(index, 1);
  }
}
