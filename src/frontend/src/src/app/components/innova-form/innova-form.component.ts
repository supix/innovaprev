import {AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule, ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {NgSelectConfig, NgSelectModule} from '@ng-select/ng-select';
import {ApiService} from '../../services/api.service';
import {CollectionsResponse, DataPayload} from '../../models';
import {finalize, startWith, Subscription, tap} from 'rxjs';
import {PriceDisplayComponent} from '../price-display/price-display.component';

@Component({
  selector: 'app-innova-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgSelectModule, PriceDisplayComponent],
  templateUrl: './innova-form.component.html',
  styleUrls: ['./innova-form.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class InnovaFormComponent implements OnInit, AfterViewInit {

  @ViewChild('tab', {static: true}) tabElement!: ElementRef;

  form: FormGroup;
  price: number | null = null;
  collections: CollectionsResponse | null = null;
  isCollectionsLoading: boolean = false;

  subscriptions: Subscription[] = [];

  submitted: boolean = false;
  hasTriggeredValidation: boolean = false;
  isLoading: boolean = false;

  currentTab: TabNames = TabNames.personal;
  tabNames = TabNames;

  constructor(private fb: FormBuilder, private config: NgSelectConfig, private apiService: ApiService) {
    this.config.bindLabel = 'desc';
    this.config.bindValue = 'id';
    this.config.notFoundText = 'Nessun elemento trovato';
    this.form = this.fb.group({
      personalData: this.fb.group({
        companyName: ['', Validators.required],
        address: ['', Validators.required],
        vat: ['', italianVatValidator()],
        phone: ['', phoneNumberValidator(true)],
        mail: ['', [Validators.required, Validators.email]],
        orderNumber: ['', Validators.required]
      }),
      productData: this.fb.group({
        product: [null, Validators.required],
        glassStopper: [false],
        windowSlide: [false],
        internalColor: [null, Validators.required],
        externalColor: [null, Validators.required],
        accessoryColor: [null, Validators.required],
        climateZone: [null, Validators.required],
        notes: ['']
      }),
      windowsData: this.fb.array([]), // Contains the rows for the window estimates
    });

    // Add an initial row for windows
    this.addRow(true);
  }

  ngOnInit(): void {
    this.apiService.getCollectionsData().pipe(
      startWith(null),
      tap(() => this.isCollectionsLoading = true),
      finalize(() => this.isCollectionsLoading = false)
    ).subscribe(collections => this.collections = collections);
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

  hasErrorsAndNotLoading(): boolean {
    if (!this.submitted) {
      return false;
    }
    if (this.submitted && this.isLoading) {
      return true;
    }
    const personalDataGroup = this.form.get('personalData');
    const hasErrorsInPersonal = personalDataGroup ? this.hasErrors(personalDataGroup) : false;

    const productDataGroup = this.form.get('productData');
    const hasErrorsInProduct = productDataGroup ? this.hasErrors(productDataGroup) : false;

    const hasErrorsInWindows = !this.areAllRowsValid();

    return (hasErrorsInPersonal || hasErrorsInProduct || hasErrorsInWindows);
  }

  // Add a new row to the windows FormArray
  addRow(skipFirst?: boolean): void {
    if (!skipFirst) {
      this.hasTriggeredValidation = true;
    }
    if (this.areAllRowsValid()) {
      const row = this.fb.group({
        position: [0],
        height: [null, minNumber(1, true, 'f')],
        width: [null, minNumber(1, true, 'f')],
        quantity: [1, minNumber(1, true, 'f')],
        windowType: [null, Validators.required],
        openingType: [null, Validators.required],
        glassType: [null, Validators.required],
        crosspiece: [null, Validators.required],
        leftTrim: [null, minNumber(0, true)],
        rightTrim: [null, minNumber(0, true)],
        upperTrim: [null, minNumber(0, true)],
        belowThreshold: [null, minNumber(0, true)],
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
      control.setValue(+value); // Update the FormControl with the filtered value
    }
  }

  // Filters non-numeric characters from the input and updates the corresponding FormControl.
  onInputNumber(event: Event, controlName: string): void {
    const input = event.target as HTMLInputElement;
    const value = input.value.replace(/[^0-9]/g, ''); // Remove all non-numeric characters
    const control = this.form.get(controlName);

    if (control) {
      control.setValue(value); // Update the FormControl with the filtered value
    }
  }

  ngSelectHandleFocus(enabled: boolean): void {
    if (enabled) {
      setTimeout(() => {
        const myCustomClass: string ="custom-table-lg"
        const panel = document.querySelector('.ng-dropdown-panel');
        if (panel){
          panel.classList.add(myCustomClass);
        }
      }, 0);
    }
  }

  // Submit the form and calculate the price
  calculatePrice(): void {
    this.submitted = true;
    if (this.form.valid) {
      this.isLoading = true;
      const payload: DataPayload = this.buildPayload();
      this.apiService.getPrice(payload).pipe(
        finalize(() => {
          this.isLoading = false;
          this.submitted = false;
        })
      ).subscribe(({quotation}) => {
        this.price = quotation?.amount;
      });
    } else {
      this.markAllTouchedAndValidate();
    }
  }

  // Download the PDF
  downloadPdf(): void {
    this.submitted = true;
    if (this.form.valid) {
      this.isLoading = true;
      const payload: DataPayload = this.buildPayload();
      this.apiService.downloadPdf(payload).pipe(
        finalize(() => {
          this.isLoading = false;
          this.submitted = false;
        })
      ).subscribe((response) => {
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

enum TabNames {
  personal = 'personal-tab',
  product = 'product-tab',
  measurements = 'measurements-tab'
}

// Checks if the value meets the minimum requirement or returns a validation error.
function minNumber(min: number, required: boolean = false, gender: 'm' | 'f' = 'm'): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // If the value is null, undefined, or an empty string
    if (value === null || value === undefined || value === '') {
      if (required) {
        return { invalidValue: { reason: ` è obbligatori${gender === 'm' ? 'o' : 'a'}` } };
      }
      return null; // Not required and empty values are allowed
    }

    // Ensure the value is numeric
    const numericValue = Number(value);
    if (isNaN(numericValue)) {
      return { invalidValue: { reason: ' deve essere un numero valido' } };
    }

    // Check if the value meets the minimum requirement
    if (numericValue < min) {
      return { invalidValue: { reason: ` è minimo di ${min}` } };
    }

    return null; // Validation passed
  };
}

// Checks the validity of the Italian Vat or returns a validation error.
function italianVatValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // Null or empty values are not validated
    if (value === null || value === undefined || value === '') {
      return { italianVat: { reason: 'La partita IVA è obbligatoria.' } };
    }

    // Ensure the value is a string of exactly 11 numeric characters
    if (!/^\d{11}$/.test(value)) {
      return { italianVat: { reason: 'La partita IVA deve essere di 11 cifre.' } };
    }

    // Validate the Italian Vat using the checksum algorithm
    if (!isValidItalianVat(value)) {
      return { italianVat: { reason: 'La partita IVA non è valida.' } };
    }

    return null;
  };
}

// Checks if the Italian Vat is valid using the checksum algorithm.
function isValidItalianVat(vat: string): boolean {
  const digits = vat.split('').map(Number);
  let sum = 0;

  for (let i = 0; i < 11; i++) {
    if (i % 2 === 0) {
      // Even positions (0-based index): add the digit as is
      sum += digits[i];
    } else {
      // Odd positions: double the digit and subtract 9 if the result is >= 10
      const doubled = digits[i] * 2;
      sum += doubled > 9 ? doubled - 9 : doubled;
    }
  }

  // The sum must be divisible by 10
  return sum % 10 === 0;
}

// Validator for phone numbers
function phoneNumberValidator(required: boolean = false, minLength: number = 8, maxLength: number = 15): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // If the value is null, undefined, or an empty string
    if (value === null || value === undefined || value === '') {
      if (required) {
        return { invalidPhoneNumber: { reason: 'Il numero di telefono è obbligatorio' } };
      }
      return null; // Not required and empty values are allowed
    }

    // Remove spaces and dashes to validate only numeric characters
    const sanitizedValue = value.replace(/[\s\-]/g, '');

    // Check if the value contains only numbers
    if (!/^\d+$/.test(sanitizedValue)) {
      return { invalidPhoneNumber: { reason: 'Il numero di telefono deve contenere solo cifre numeriche.' } };
    }

    // Check the length requirements
    if (sanitizedValue.length < minLength || sanitizedValue.length > maxLength) {
      return {
        invalidPhoneNumber: {
          reason: `Il numero di telefono è troppo breve.`,
        },
      };
    }

    return null; // Validation passed
  };
}
