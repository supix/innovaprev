import {AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {NgSelectConfig, NgSelectModule} from '@ng-select/ng-select';
import {ApiService} from '../../services/api.service';
import {CollectionsResponse, DataPayload, WindowsPayload} from '../../models';
import {debounceTime, EMPTY, finalize, startWith, Subject, Subscription, switchMap, tap} from 'rxjs';
import {PriceDisplayComponent} from '../price-display/price-display.component';
import {italianVatValidator, minNumber, phoneNumberValidator} from '../../validators/innova.validator';

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

  private calculatePriceSubject = new Subject<WindowsPayload | null>();

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
    this.setupPriceCalculationSubscription();
    this.subscribeToWindowsChanges();
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

  // Helper function to check validity of a specific row by index
  isRowValid(index: number): boolean {
    const row = this.windows.at(index);
    return row ? row.valid : false;
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
        leftTrim: [null, minNumber(0)],
        rightTrim: [null, minNumber(0)],
        upperTrim: [null, minNumber(0)],
        belowThreshold: [null, minNumber(0)],
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
      this.onMaxQuantity(event, controlName);
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

  // Handler to have limits on some form inputs
  onMaxQuantity(event: Event, controlName: string): void {
    const maxValues: { [key: string]: number } = {
      height: 5000,
      width: 5000,
      quantity: 500,
      leftTrim: 500,
      rightTrim: 500,
      upperTrim: 500,
      belowThreshold: 500
    };

    const max: number = maxValues[controlName];
    if (max === undefined) {
      return; // Exit if no max value is defined for the control
    }

    const input = event.target as HTMLInputElement;
    const value = parseInt(input.value, 10); // Parse the input value as an integer

    if (isNaN(value)) {
      return; // Exit if the input value is not a number
    }

    if (value > max) {
      input.value = max.toString(); // Update the input field value to the maximum allowed value
      const control = this.form.get(controlName);
      if (control) {
        control.setValue(max); // Update the FormControl with the maximum allowed value
      }
    }
  }

  // Needed to work around a ng-select issue
  ngSelectHandleFocus(enabled: boolean): void {
    if (enabled) {
      setTimeout(() => {
        const myCustomClass: string = "custom-table-lg"
        const panel = document.querySelector('.ng-dropdown-panel');
        if (panel) {
          panel.classList.add(myCustomClass);
        }
      }, 0);
    }
  }

  calculatePriceHandler(): void {
    const validRows = this.getValidWindowsData();
    if (validRows.length > 0) {
      const payload: WindowsPayload = this.buildWindowsPayload();
      this.calculatePriceSubject.next(payload);
    } else {
      this.calculatePriceSubject.next(null);
    }
  }

  // Setup subscription to calculate price
  private setupPriceCalculationSubscription(): void {
    this.calculatePriceSubject.pipe(
      debounceTime(300), // Delay to avoid frequent calls
      switchMap((payload) => {
        if (payload === null) {
          this.price = null;
          return EMPTY;
        }
        this.isLoading = true;
        return this.apiService.getPrice(payload).pipe(
          finalize(() => {
            this.isLoading = false;
          })
        );
      })
    ).subscribe(({quotation}) => {
      this.price = quotation?.amount;
    });
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

  // Adds tooltip on mouse enter
  onEnterTooltip(event: MouseEvent, tooltipId: string): void {
    const tooltip = document.getElementById(tooltipId);
    const target = event.target as HTMLElement;

    if (tooltip && target) {
      const rect = target.getBoundingClientRect();

      tooltip.style.top = `${rect.bottom + 5}px`;
      tooltip.style.left = `${rect.left + rect.width / 2}px`;
      tooltip.style.transform = 'translateX(-50%)';

      tooltip.style.opacity = '1';
    }
  }

  // Removes the tooltip on the mouse leave
  onLeaveTooltip(tooltipId: string): void {
    const tooltip = document.getElementById(tooltipId);

    if (tooltip) {
      tooltip.style.opacity = '0';
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

  // Helper function to get valid rows from the FormArray
  private getValidWindowsData(): any[] {
    return this.windows.controls
      .filter(row => row.valid)
      .map(row => row.value);
  }

  // Build Data payload for API
  private buildPayload(): DataPayload {
    return {
      personalData: this.form.value.personalData,
      productData: this.form.value.productData,
      ...this.buildWindowsPayload()
    };
  }

  // Build Windows payload for API using valid rows from the form
  private buildWindowsPayload(): WindowsPayload {
    const validRows = this.getValidWindowsData();
    return {
      windowsData: validRows
    };
  }

  // Mark all fields as touched to show validation errors
  private markAllTouchedAndValidate(): void {
    this.form.markAllAsTouched();
    if (!this.areAllRowsValid()) {
      this.hasTriggeredValidation = true;
    }
  }

  // Subscribe to changes in the FormArray
  private subscribeToWindowsChanges(): void {
    this.windows.valueChanges.subscribe(() => {
      this.calculatePriceHandler();
    });
  }

}

enum TabNames {
  personal = 'personal-tab',
  product = 'product-tab',
  measurements = 'measurements-tab'
}
