import {AfterViewInit, Component, ElementRef, ViewChild} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ApiService} from '../services/api.service';
import {DataPayload} from '../models';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-innova-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './innova-form.component.html',
  styleUrls: ['./innova-form.component.scss'],
})
export class InnovaFormComponent implements AfterViewInit {

  @ViewChild('tab', {static: true}) tabElement!: ElementRef;

  form: FormGroup;
  price: number | null = null;
  subscriptions: Subscription[] = [];

  submitted: boolean = false;
  hasTriggeredValidation: boolean = false;
  isLoading: boolean = false;

  currentTab: TabNames = TabNames.personal;

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.form = this.fb.group({
      personalData: this.fb.group({
        companyName: ['', Validators.required],
        address: ['', Validators.required],
        vat: ['', Validators.required],
        phone: ['', Validators.required],
        mail: ['', [Validators.required, Validators.email]],
        orderNumber: ['', Validators.required],
      }),
      productData: this.fb.group({
        product: ['', Validators.required],
        glassStopper: [false], // Checkbox
        windowSlide: [false], // Checkbox
        internalColor: ['', Validators.required],
        externalColor: ['', Validators.required],
        accessoryColor: ['', Validators.required],
        climateZone: ['', Validators.required],
        notes: [''],
      }),
      windowsData: this.fb.array([]), // Contains the rows for the window estimates
    });

    // Add an initial row for windows
    this.addRow(true);
  }

  ngAfterViewInit(): void {
    const tabEl = this.tabElement.nativeElement;
    tabEl.addEventListener('shown.bs.tab', (event: any) => {
      this.currentTab = event.target.id;
    });
  }

  // Getter for the windows FormArray
  get windows(): FormArray {
    return this.form.get('windowsData') as FormArray;
  }

  addRowCheck(): boolean {
    return this.hasTriggeredValidation;
  }

  hasErrorsInPersonalData(): boolean {
    const personalDataGroup = this.form.get('personalData');
    return this.currentTab !== TabNames.personal && this.submitted && this.hasErrors(personalDataGroup);
  }

  hasErrorsInProductData(): boolean {
    const productDataGroup = this.form.get('productData');
    return this.currentTab !== TabNames.product && this.submitted && this.hasErrors(productDataGroup);
  }

  hasErrorsInWindowsData(): boolean {
    return this.currentTab !== TabNames.measurements && this.hasTriggeredValidation && !this.areAllRowsValid();
  }

  areAllRowsValid(): boolean {
    return this.windows.controls.every((row) => row.valid);
  }

  // Add a new row to the windows FormArray
  addRow(skipFirst?: boolean): void {
    if (!skipFirst) {
      this.hasTriggeredValidation = true;
    }
    if (this.areAllRowsValid()) {
      const row = this.fb.group({
        position: [0],
        height: [0, [Validators.required, Validators.min(1)]],
        width: [0, [Validators.required, Validators.min(1)]],
        quantity: [1, [Validators.required, Validators.min(1)]],
        windowType: ['', Validators.required],
        openingType: ['', Validators.required],
        glassType: ['', Validators.required],
        crosspiece: ['', Validators.required],
        leftTrim: [0],
        rightTrim: [0],
        upperTrim: [0],
        belowThreshold: [0],
      });
      this.windows.push(row);
      this.updatePositions();
      this.subscribeToRowValueChanges(row, this.windows.length - 1);
    }
  }

  // Remove a specific row from the windows FormArray
  removeRow(index: number): void {
    this.windows.removeAt(index);
    this.subscriptions[index]?.unsubscribe();
    this.subscriptions.splice(index, 1);
    this.updatePositions();
    if (this.windows.length === 0) {
      this.addRow(true);
    }
  }

  // Updates the value of the `position` field for each row in the FormArray.
  updatePositions(): void {
    this.windows.controls.forEach((group, index) => {
      group.get('position')?.setValue(index + 1);
    });
  }

  // Filters non-numeric characters from the input and updates the corresponding FormControl.
  onRowInputNumber(event: Event, index: number, controlName: string): void {
    const input = event.target as HTMLInputElement;
    const value = input.value.replace(/[^0-9]/g, ''); // Remove all non-numeric characters
    const control = this.windows.at(index).get(controlName);

    if (control) {
      control.setValue(value); // Update the FormControl with the filtered value
    }
  }

  // Submit the form and calculate the price
  calculatePrice(): void {
    this.submitted = true;
    this.isLoading = true;
    if (this.form.valid) {
      const payload: DataPayload = this.buildPayload();
      this.apiService.getPrice(payload).subscribe(({totalEstimatedPrice}) => {
        this.isLoading = false;
        this.price = totalEstimatedPrice;
      });
    } else {
      this.isLoading = false;
      this.markAllTouchedAndValidate();
    }
  }

  // Download the PDF
  downloadPdf(): void {
    this.submitted = true;
    this.isLoading = true;
    if (this.form.valid) {
      const payload: DataPayload = this.buildPayload();
      this.apiService.downloadPdf(payload).subscribe((response) => {
        this.isLoading = false;
        const blob = new Blob([response], {type: 'application/pdf'});
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        const timestamp = Date.now();
        a.href = url;
        a.download = `estimate-${timestamp}.pdf`;
        a.click();
        window.URL.revokeObjectURL(url);
      });
    } else {
      this.isLoading = false;
      this.markAllTouchedAndValidate();
    }
  }

  // Subscribes to `valueChanges` for a specific row
  private subscribeToRowValueChanges(row: FormGroup, index: number): void {
    const subscription = row.valueChanges.subscribe(() => {
      this.onRowValueChanged(index);
    });
    this.subscriptions.push(subscription);
  }

  // Function executed whenever the values in a row change.
  private onRowValueChanged(index: number): void {
    const row = this.windows.at(index);

    if (row && row.valid && this.hasTriggeredValidation) {
      this.hasTriggeredValidation = false;
    }
  }

  // Generic method to check if there are errors in a FormGroup
  private hasErrors(group: AbstractControl | null): boolean {
    if (!group) return false;

    if (group.invalid) return true;

    if (group instanceof FormGroup) {
      return Object.values(group.controls).some(control => this.hasErrors(control));
    }

    return false;
  }

  // Build payload for API
  private buildPayload(): DataPayload {
    return {
      personalData: this.form.value.personalData,
      productData: this.form.value.productData,
      windowsData: this.form.value.windowsData,
    };
  }

  // Mark all fields as touched to show validation errors
  private markAllTouchedAndValidate(): void {
    this.form.markAllAsTouched();
    if (!this.areAllRowsValid()) {
      this.hasTriggeredValidation = true;
    }
  }

}

export enum TabNames {
  personal = 'personal-tab',
  product = 'product-tab',
  measurements = 'measurements-tab'
}

