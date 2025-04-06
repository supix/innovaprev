import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgSelectConfig, NgSelectModule } from '@ng-select/ng-select';
import { ApiService } from '../../services/api.service';
import {
  BillingPayload,
  CollectionBaseItem,
  CollectionsResponse,
  Colors,
  CustomPayload,
  PricePayload,
  Quotation,
  WindowsPayload,
  WindowType
} from '../../models';
import {
  combineLatest,
  debounceTime,
  EMPTY,
  filter,
  finalize,
  startWith,
  Subject,
  Subscription,
  switchMap,
  tap
} from 'rxjs';
import { PriceDisplayComponent } from '../price-display/price-display.component';
import {
  bankCoordinatesValidator,
  generateValidItalianVat,
  italianVatValidator,
  minNumber,
  phoneNumberValidator
} from '../../validators/innova.validator';
import { catchError } from 'rxjs/operators';
import CryptoJS from 'crypto-js';
import { environment } from '../../../environments/environment';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-innova-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgSelectModule, PriceDisplayComponent],
  templateUrl: './innova-form.component.html',
  styleUrls: ['./innova-form.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class InnovaFormComponent implements OnInit, AfterViewInit, OnDestroy {

  @ViewChild('tab', {static: true}) tabElement!: ElementRef;

  form: FormGroup;
  quotation: Quotation | null = null;
  collections: CollectionsResponse | null = null;
  isCollectionsLoading: boolean = false;

  windowSubscriptions: Subscription[] = [];
  customSubscriptions: Subscription[] = [];

  submitted: boolean = false;
  hasTriggeredWindowsValidation: boolean = false;
  hasTriggeredCustomValidation: boolean = false;
  isLoading: boolean = false;

  currentTab: TabNames = TabNames.supplier;
  tabNames = TabNames;

  maxValues: { [key: string]: number } = {
    height: 5000,
    width: 5000,
    length: 5000,
    quantity: 500,
    leftTrim: 500,
    rightTrim: 500,
    upperTrim: 500,
    belowThreshold: 500
  };

  showFillFormButton = false;

  externalColorList: Colors[] = [];
  internalColorList: Colors[] = [];
  windowTypeList: WindowType[] = [];

  private selectedProductId!: string | null;
  private calculatePriceSubject = new Subject<PricePayload | null>();
  private previousPayloadHash: string | null = null;

  constructor(private sanitizer: DomSanitizer, private fb: FormBuilder,
              private config: NgSelectConfig, private apiService: ApiService) {
    this.config.bindLabel = 'desc';
    this.config.bindValue = 'id';
    this.config.notFoundText = 'Nessun elemento trovato';
    this.config.clearAllText = 'Pulisci tutto';
    this.form = this.fb.group({
      supplierData: this.fb.group({
        companyName: ['', Validators.required],
        address: [''],
        taxCode: ['', italianVatValidator()],
        phone: ['', phoneNumberValidator(false)],
        mail: ['', [Validators.email]],
        iban: ['', bankCoordinatesValidator()],
      }),
      customerData: this.fb.group({
        companyName: ['', Validators.required],
        address: [''],
        taxCode: ['', italianVatValidator()],
        phone: ['', phoneNumberValidator(false)],
        mail: ['', [Validators.email]],
        iban: ['', bankCoordinatesValidator()],
      }),
      productData: this.fb.group({
        orderNumber: [''],
        product: [null, Validators.required],
        internalColor: [{value: null, disabled: true}, Validators.required],
        externalColor: [{value: null, disabled: true}, Validators.required],
        accessoryColor: [null, Validators.required],
        notes: ['']
      }),
      windowsData: this.fb.array([]), // Contains the rows for the window estimates
      customData: this.fb.array([]), // Contains the rows for the custom data
    });

    // Add an initial row for windows
    this.addWindowRow(true);

  }

  ngOnInit(): void {
    this.apiService.getCollectionsData().pipe(
      startWith(null),
      tap(() => this.isCollectionsLoading = true),
      finalize(() => this.isCollectionsLoading = false)
    ).subscribe(collections => this.collections = collections);
    this.setupPriceCalculationSubscription();
    this.subscribeToFormChanges();
    this.showFillFormButton = this.determineShowFillFormButton();
  }

  ngAfterViewInit(): void {
    const tabEl = this.tabElement.nativeElement;
    tabEl.addEventListener('shown.bs.tab', (event: any) => {
      this.currentTab = event.target.id;
    });
  }

  ngOnDestroy(): void {
    this.windowSubscriptions.forEach(subscription => subscription.unsubscribe());
    this.customSubscriptions.forEach(subscription => subscription.unsubscribe());
  }

  // Getter for the supplier FormGroup
  get supplierData(): FormGroup {
    return this.form.get('supplierData') as FormGroup;
  }

  // Getter for the customer FormGroup
  get customerData(): FormGroup {
    return this.form.get('customerData') as FormGroup;
  }

  // Getter for the product FormGroup
  get productData(): FormGroup {
    return this.form.get('productData') as FormGroup;
  }

  // Getter for the windows FormArray
  get windows(): FormArray {
    return this.form.get('windowsData') as FormArray;
  }

  // Getter for the windows FormArray
  get customData(): FormArray {
    return this.form.get('customData') as FormArray;
  }

  sanitizeHtml(text: string): SafeHtml {
    const htmlContent = text.replace(/\n/g, '<br>');
    return this.sanitizer.bypassSecurityTrustHtml(htmlContent);
  }

  isTrimSectionVisible(): boolean {
    const currentProduct = this.productData.value['product'];
    return this.collections?.product.some(p => p.id === currentProduct && p.trimSectionVisible) || false;
  }

  isWindowRowValid(index: number): boolean {
    const row = this.windows.at(index) as FormGroup;
    if (!row) return false;

    const isStandardValid = row.valid;

    const allDisabledRequiredFieldsValid = Object.keys(row.controls).every(controlName => {
      const control = row.get(controlName);
      const isDisabled = control?.disabled;
      const isRequired = control?.hasValidator(Validators.required);

      return !isDisabled || !isRequired || !!control.value;
    });

    return isStandardValid && allDisabledRequiredFieldsValid;
  }

  // Helper function to check validity of a specific row by index
  isCustomRowValid(index: number): boolean {
    const row = this.customData.at(index);
    return row ? row.valid : false;
  }

  addWindowRowCheck(): boolean {
    return this.hasTriggeredWindowsValidation;
  }

  addCustomRowCheck(): boolean {
    return this.hasTriggeredCustomValidation;
  }

  hasErrorsInSupplierData(): boolean {
    const supplierDataGroup = this.supplierData;
    return this.currentTab !== TabNames.supplier && this.submitted && this.hasErrors(supplierDataGroup);
  }

  hasErrorsInCustomerData(): boolean {
    const customerDataGroup = this.customerData;
    return this.currentTab !== TabNames.customer && this.submitted && this.hasErrors(customerDataGroup);
  }

  hasErrorsInProductData(): boolean {
    const productDataGroup = this.productData;
    return this.currentTab !== TabNames.product && this.submitted && this.hasErrors(productDataGroup);
  }

  hasErrorsInWindowsData(): boolean {
    return this.currentTab !== TabNames.measurements && this.hasTriggeredWindowsValidation && !this.areAllWindowRowsValid();
  }

  hasErrorsInCustomData(): boolean {
    return this.currentTab !== TabNames.customData && this.hasTriggeredCustomValidation && !this.areAllCustomRowsValid();
  }

  areAllWindowRowsValid(): boolean {
    return this.windows.controls.every((row) => row.valid);
  }

  areAllCustomRowsValid(): boolean {
    return this.customData.controls.every((row) => row.valid);
  }

  hasErrorsAndNotLoading(): boolean {
    if (!this.submitted) {
      return false;
    }
    if (this.submitted && this.isLoading) {
      return true;
    }

    const supplierDataGroup = this.supplierData;
    const hasErrorsInSupplier = supplierDataGroup ? this.hasErrors(supplierDataGroup) : false;

    const customerDataGroup = this.customerData;
    const hasErrorsInCustomer = customerDataGroup ? this.hasErrors(customerDataGroup) : false;

    const productDataGroup = this.productData;
    const hasErrorsInProduct = productDataGroup ? this.hasErrors(productDataGroup) : false;

    const hasErrorsInWindows = !this.areAllWindowRowsValid();

    const hasErrorsInCustomData = !this.areAllCustomRowsValid();

    return (hasErrorsInSupplier || hasErrorsInCustomer || hasErrorsInProduct || hasErrorsInWindows || hasErrorsInCustomData);
  }

  onChangeProduct(selectedProductId: string): void {
    this.selectedProductId = selectedProductId || null;
    this.internalColorList = this.getInternalColors();
    this.externalColorList = this.getExternalColors();
    this.windowTypeList = this.getWindowTypes();
    this.updateFormState();
    this.updateWindowsFormState();
  }

  onChangeInternalColor($event: Colors): void {
    if (!this.isSingleColor()) {
      return;
    }
    const externalColorControl = this.productData.get('externalColor');
    if (externalColorControl) {
      externalColorControl.setValue($event.id, {emitEvent: false});
      externalColorControl.updateValueAndValidity({emitEvent: false});
    }
  }

  getInternalColors(): Colors[] {
    if (!this.selectedProductId || !this.collections?.colors) {
      return [];
    }

    return this.collections.colors
      .filter(color => color.internalColorForProduct.includes(this.selectedProductId as string));
  }

  getExternalColors(): Colors[] {
    if (!this.selectedProductId || !this.collections?.colors) {
      return [];
    }

    return this.collections.colors
      .filter(color => color.externalColorForProduct.includes(this.selectedProductId as string));
  }

  getWindowTypes(): WindowType[] {
    if (!this.selectedProductId || !this.collections?.windowTypes) {
      return [];
    }

    return this.collections.windowTypes
      .filter(color => color.materialForProduct.includes(this.selectedProductId as string));
  }

  // Add a new row to the windows FormArray
  addWindowRow(skipFirst?: boolean): void {
    if (!skipFirst) {
      this.hasTriggeredWindowsValidation = true;
    }
    if (this.areAllWindowRowsValid()) {
      const row = this.fb.group({
        position: [0],
        height: [{value: null, disabled: true}, minNumber(1, true, 'f')],
        width: [{value: null, disabled: true}, minNumber(1, true, 'f')],
        length: [{value: null, disabled: true}, minNumber(1, true, 'f')],
        quantity: [1, minNumber(1, true, 'f')],
        windowType: [{value: null, disabled: true}, Validators.required],
        openingType: [{value: null, disabled: true}, Validators.required],
        glassType: [{value: null, disabled: true}, Validators.required],
        leftTrim: [null, minNumber(0)],
        rightTrim: [null, minNumber(0)],
        upperTrim: [null, minNumber(0)],
        belowThreshold: [null, minNumber(0)],
      });
      this.windows.push(row);
      this.updateWindowPositions();
      this.updateWindowsFormState();
      this.subscribeToWindowRowValueChanges(row, this.windows.length - 1);
    }
  }

  // Remove a specific row from the windows FormArray
  removeWindowRow(index: number): void {
    this.windows.removeAt(index);
    this.windowSubscriptions[index]?.unsubscribe();
    this.windowSubscriptions.splice(index, 1);
    this.updateWindowPositions();
    if (this.windows.length === 0) {
      this.addWindowRow(true);
      this.updateWindowsFormState();
    }
  }

  // Updates the value of the `position` field for each row in the FormArray.
  updateWindowPositions(): void {
    this.windows.controls.forEach((group, index) => {
      group.get('position')?.setValue(index + 1);
    });
  }

  // Add a new row to the windows FormArray
  addCustomRow(skipFirst?: boolean): void {
    if (!skipFirst) {
      this.hasTriggeredCustomValidation = true;
    }
    if (this.areAllCustomRowsValid()) {
      const row = this.fb.group({
        position: [0],
        description: ['', Validators.required],
        quantity: ['', Validators.required],
        price: [null, minNumber(1, true, 'm')]
      });
      this.customData.push(row);
      this.updateCustomPositions();
      this.subscribeToCustomRowValueChanges(row, this.customData.length - 1);
    }
  }

  // Remove a specific row from the windows FormArray
  removeCustomRow(index: number): void {
    this.customData.removeAt(index);
    this.customSubscriptions[index]?.unsubscribe();
    this.customSubscriptions.splice(index, 1);
    this.updateCustomPositions();
  }

  // Updates the value of the `position` field for each row in the FormArray.
  updateCustomPositions(): void {
    this.customData.controls.forEach((group, index) => {
      group.get('position')?.setValue(index + 1);
    });
  }

  // Filters non-numeric characters from the input and updates the corresponding FormControl.
  onWindowsRowInputNumber(event: Event, index: number, controlName: string): void {
    const input = event.target as HTMLInputElement;
    const value = input.value.replace(/[^0-9]/g, ''); // Remove all non-numeric characters
    const control = this.windows.at(index).get(controlName);

    if (control) {
      control.setValue(+value); // Update the FormControl with the filtered value
      this.onMaxQuantity(event, controlName);
    }
  }

  // Filters non-numeric characters from the input and updates the corresponding FormControl.
  onWindowsRowClearInput(index: number, controlName: string): void {
    const control = this.windows.at(index).get(controlName);

    if (control) {
      control.setValue(null);
    }
  }

  // Handles numeric input with support for up to two decimals and proper FormControl updates.
  onPriceInputNumber(event: Event, index: number, controlName: string): void {
    const input = event.target as HTMLInputElement;
    let value = input.value;

    // Allow numbers, one comma or dot as a decimal separator, and limit to two decimals
    const validValue = value.match(/^\d*[.,]?\d{0,2}$/) ? value : input.dataset['previousValue'] || '';

    // Save the valid value as the last valid state
    input.dataset['previousValue'] = validValue;

    // Replace commas with dots for consistency in decimal values
    const numericValue = validValue.replace(',', '.');

    // Update the FormControl if the numericValue is valid
    const control = this.customData.at(index).get(controlName);
    if (control) {
      const parsedValue = parseFloat(numericValue);
      control.setValue(!isNaN(parsedValue) ? parsedValue : null); // Set null for invalid input
    }

    // Update the input value to reflect the current valid value
    input.value = validValue;
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

  // Needed to work around a ng-select issue
  ngSelectHandleFocus(enabled: boolean, myCustomClass: string = "custom-table-lg"): void {
    if (enabled) {
      setTimeout(() => {
        const panel = document.querySelector('.ng-dropdown-panel');
        if (panel) {
          panel.classList.add(myCustomClass);
        }
      }, 0);
    }
  }

  // Download the PDF
  downloadPdf(): void {
    this.submitted = true;
    if (this.form.valid) {
      this.isLoading = true;
      const payload: BillingPayload = this.buildBillingPayload();
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

  getUrl(productCode: string, isThumb: boolean = true): string {
    return this.apiService.getImageUrl(productCode, isThumb);
  }

  fillForm(): void {
    if (!this.collections) {
      console.error('Collections data is not loaded');
      return;
    }

    const currentFormValue = this.form.value;

    const product = currentFormValue.productData?.product || getRandomItem(this.collections.product, null);

    if (!product) {
      console.error('Product not found!');
      return;
    } else {
      console.log('Product found: ', product);
      this.onChangeProduct(product);
    }

    const getRandomWindowType = (): string => {
      const windowTypeList = this.getWindowTypes();
      return windowTypeList?.length > 0 ? windowTypeList[Math.floor(Math.random() * windowTypeList.length)].id : 'defaultType';
    }

    const getRandomInternalColor = (): string => {
      const internalColorList = this.getInternalColors();
      return internalColorList?.length > 0 ? internalColorList[Math.floor(Math.random() * internalColorList.length)].id : 'defaultInternalColor';
    }

    const getRandomExternalColor = (): string => {
      const externalColorList = this.getExternalColors();
      return externalColorList?.length > 0 ? externalColorList[Math.floor(Math.random() * externalColorList.length)].id : 'defaultExternalColor';
    }

    // Filter out invalid windows
    const validWindows = this.windows.controls.filter(control => control.valid);
    while (this.windows.length > 0) {
      const index = this.windows.length - 1;
      this.windows.removeAt(index);
      this.windowSubscriptions[index]?.unsubscribe();
      this.windowSubscriptions.splice(index, 1);
    }

    // Add back only the valid windows
    validWindows.forEach(validControl => {
      this.windows.push(validControl);
      this.subscribeToWindowRowValueChanges(validControl as FormGroup, this.windows.length - 1);
    });

    // Add 5 new windows to windowsData
    for (let i = 0; i < 5; i++) {
      const row = this.fb.group({
        position: [0], // Updated later by updateWindowPositions
        height: [getRandomNumber(this.maxValues['height'], 500)],
        width: [getRandomNumber(this.maxValues['width'], 500)],
        length: [getRandomNumber(this.maxValues['width'], 500)],
        quantity: [getRandomNumber(5)],
        windowType: [getRandomWindowType(), Validators.required],
        openingType: [getRandomItem(this.collections.openingTypes, 'defaultOpening'), Validators.required],
        glassType: [getRandomItem(this.collections.glassTypes, 'defaultGlass'), Validators.required],
        leftTrim: [getRandomNumber(20)],
        rightTrim: [getRandomNumber(20)],
        upperTrim: [getRandomNumber(20)],
        belowThreshold: [getRandomNumber(20)]
      });
      this.windows.push(row);
      this.subscribeToWindowRowValueChanges(row, this.windows.length - 1);
    }
    this.updateWindowPositions();

    // Filter out invalid components
    const validCustomData = this.customData.controls.filter(control => control.valid);
    while (this.customData.length > 0) {
      const index = this.customData.length - 1;
      this.customData.removeAt(index);
      this.customSubscriptions[index]?.unsubscribe();
      this.customSubscriptions.splice(index, 1);
    }

    // Add back only the valid components
    validCustomData.forEach(validControl => {
      this.customData.push(validControl);
      this.subscribeToCustomRowValueChanges(validControl as FormGroup, this.customData.length - 1);
    });
    // Add 5 new components to customData
    for (let i = 0; i < 5; i++) {
      const row = this.fb.group({
        position: [0], // Updated later by updateCustomPositions
        quantity: ['n. ' + getRandomNumber(5)],
        description: ['Lorem ipsum dolor sit amet, consectetur adipiscing elit'],
        price: [getRandomNumber(500), Validators.required],
      });
      this.customData.push(row);
      this.subscribeToCustomRowValueChanges(row, this.customData.length - 1);
    }
    this.updateCustomPositions();

    // Patch other form values
    this.form.patchValue({
      supplierData: {
        companyName: currentFormValue.supplierData?.companyName || 'Supplier Co.',
        address: currentFormValue.supplierData?.address || '123 Supplier Street',
        taxCode: currentFormValue.supplierData?.taxCode || generateValidItalianVat(),
        phone: currentFormValue.supplierData?.phone || '391234567890',
        mail: currentFormValue.supplierData?.mail || 'supplier@example.com',
        iban: currentFormValue.supplierData?.iban || 'IT60X0542811101000000123456'
      },
      customerData: {
        companyName: currentFormValue.customerData?.companyName || 'Customer Co.',
        address: currentFormValue.customerData?.address || '456 Customer Road',
        taxCode: currentFormValue.customerData?.taxCode || generateValidItalianVat(),
        phone: currentFormValue.customerData?.phone || '399876543210',
        mail: currentFormValue.customerData?.mail || 'customer@example.com',
        iban: currentFormValue.customerData?.iban || 'IT70X0542811101000000654321'
      },
      productData: {
        orderNumber: currentFormValue.productData?.orderNumber || 'ORD123456',
        product,
        internalColor: currentFormValue.productData?.internalColor || getRandomInternalColor(),
        externalColor: currentFormValue.productData?.externalColor || getRandomExternalColor(),
        accessoryColor: currentFormValue.productData?.accessoryColor || getRandomItem(this.collections.accessoryColors, 'defaultAccessoryColor'),
        notes: currentFormValue.productData?.notes || 'No special notes.'
      }
    });

    this.hasTriggeredWindowsValidation = false;
    this.hasTriggeredCustomValidation = false;

    // Helper function to pick a random item from an array
    function getRandomItem<T extends CollectionBaseItem>(items: T[], defaultValue: string | null = null): string | null {
      return items?.length > 0 ? items[Math.floor(Math.random() * items.length)].id : defaultValue;
    }

    // Helper function to generate a random number within a range
    function getRandomNumber(max: number, min: number = 1): number {
      return Math.max(min, Math.floor(Math.random() * max));
    }

  }

  onReset(): void {
    this.form.reset();

    while (this.windows.length > 0) {
      this.windows.removeAt(0);
    }

    while (this.customData.length > 0) {
      this.customData.removeAt(0);
    }

    this.addWindowRow(true);
    this.hasTriggeredWindowsValidation = false;
    this.hasTriggeredCustomValidation = false;
    this.submitted = false;

    this.calculatePriceSubject.next(null);
  }

  // Handler to calculate the price based on valid rows
  private calculatePriceHandler(): void {
    const validWindowRows = this.getValidWindowsData();
    const validCustomRows = this.getValidCustomData();
    if ((validWindowRows.length + validCustomRows.length) > 0 && this.isProductDataValid()) {
      const payload: PricePayload = this.buildPayload();
      this.calculatePriceSubject.next(payload);
    } else {
      this.calculatePriceSubject.next(null);
    }
  }

  // Handler to have limits on some form inputs
  private onMaxQuantity(event: Event, controlName: string): void {
    const max: number = this.maxValues[controlName];
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

  // Function executed whenever the windowType in a row change.
  onChangeWindowType(index: number): void {
    const row = this.windows.at(index) as FormGroup;
    if (!row) return;

    const windowTypeId = String(row.get('windowType')?.value);
    const found = this.collections?.windowTypes.find(wt => wt.id === windowTypeId);
    console.log(found);
    if (!found) return;

    const { numOfDims, openingTypeVisible, glassTypeVisible } = found;

    const setField = (name: string, enable: boolean, validators: any[] | null) => {
      const ctrl = row.get(name);
      if (!ctrl) return;
      ctrl.setValidators(validators);
      ctrl[enable ? 'enable' : 'disable']({ emitEvent: false });
      if (!enable) row.patchValue({ [name]: null }, { emitEvent: false });
    };

    setField('openingType', openingTypeVisible, openingTypeVisible ? [Validators.required] : null);
    setField('glassType', glassTypeVisible, glassTypeVisible ? [Validators.required] : null);

    if (numOfDims === 2) {
      setField('height', true, [minNumber(1, true, 'f')]);
      setField('width', true, [minNumber(1, true, 'f')]);
      setField('length', false, null);
    } else if (numOfDims === 1) {
      setField('height', false, null);
      setField('width', false, null);
      setField('length', true, [minNumber(1, true, 'f')]);
    }

    ['height', 'width', 'length', 'glassType', 'openingType'].forEach(field => {
      row.get(field)?.updateValueAndValidity({ emitEvent: false });
    });
  }


  // Check if the 'productData' form is valid
  private isProductDataValid(): boolean {
    return this.productData.valid;
  }

  private isSingleColor(): boolean {
    if (!this.selectedProductId || !this.collections?.product) {
      return false;
    }
    return !!this.collections?.product.find(p => p.id === this.selectedProductId)?.singleColor;
  }

  private updateFormState(): void {
    if (!this.productData) {
      return;
    }

    const internalColorControl = this.productData.get('internalColor');
    if (internalColorControl) {
      const selectedInternalColor = internalColorControl.value;

      if (this.internalColorList.length === 0) {
        internalColorControl.disable();
        internalColorControl.setValue(null, {emitEvent: false});
        internalColorControl.clearValidators();
      } else {
        const isValidInternalColor = this.internalColorList.some(color => color.id === selectedInternalColor);
        if (!isValidInternalColor) {
          internalColorControl.setValue(null, {emitEvent: false});
        }
        internalColorControl.enable();
        internalColorControl.setValidators([Validators.required]);
      }
      internalColorControl.updateValueAndValidity({emitEvent: false});
    }

    const externalColorControl = this.productData.get('externalColor');
    if (externalColorControl) {
      const selectedExternalColor = externalColorControl.value;

      if (this.externalColorList.length === 0) {
        externalColorControl.disable();
        externalColorControl.setValue(null, {emitEvent: false});
        externalColorControl.clearValidators();
      } else {
        if (this.isSingleColor()) {
          const isValidInternalColor = this.internalColorList.some(color => color.id === selectedExternalColor);
          if (!isValidInternalColor) {
            externalColorControl.setValue(null, {emitEvent: false});
          } else {
            externalColorControl.setValue(internalColorControl?.value, {emitEvent: false});
          }
          externalColorControl.disable();
        } else {
          const isValidExternalColor = this.externalColorList.some(color => color.id === selectedExternalColor);
          if (!isValidExternalColor) {
            externalColorControl.setValue(null, {emitEvent: false});
          }
          externalColorControl.enable();
        }
        externalColorControl.setValidators([Validators.required]);
      }
      externalColorControl.updateValueAndValidity({emitEvent: false});
    }
  }

  private updateWindowsFormState(): void {
    if (!this.windows) {
      return;
    }

    this.windows.controls.forEach((windowGroup: AbstractControl) => {
      const windowTypeControl = windowGroup.get('windowType');

      if (windowTypeControl) {
        const selectedWindowType = windowTypeControl.value;

        if (this.windowTypeList.length === 0) {
          windowTypeControl.disable();
          windowTypeControl.setValue(null, {emitEvent: false});
          windowTypeControl.clearValidators();
        } else {
          const isValidWindowType = this.windowTypeList.some(type => type.id === selectedWindowType);
          if (!isValidWindowType) {
            windowTypeControl.setValue(null, {emitEvent: false});
          }
          windowTypeControl.enable();
          windowTypeControl.setValidators([Validators.required]);
        }

        windowTypeControl.updateValueAndValidity({emitEvent: false});
      }
    });
  }

  // Setup subscription to calculate price
  private setupPriceCalculationSubscription(): void {
    this.calculatePriceSubject.pipe(
      debounceTime(300), // Delay to avoid frequent calls
      filter((payload) => {
        if (payload === null) {
          this.previousPayloadHash = null; // Reset hash if payload is null
          return true; // Allow null payload to pass through
        }

        const currentPayloadHash = this.calculateHash(payload);

        if (this.previousPayloadHash === currentPayloadHash) {
          return false; // Payload has not changed, skipping calculation.
        }

        this.previousPayloadHash = currentPayloadHash;
        return true; // Allow new payload to pass through
      }),
      switchMap((payload) => {
        if (payload === null) {
          this.quotation = null;
          return EMPTY;
        }
        this.isLoading = true;
        return this.apiService.getPrice(payload).pipe(
          finalize(() => {
            this.isLoading = false;
          }),
          catchError((error) => {
            console.error('Error fetching price:', error);
            this.quotation = null; // Reset price in case of error
            return EMPTY;
          })
        );
      })
    ).subscribe(({quotation}) => {
      this.quotation = quotation;
    });
  }

  // Subscribes to `valueChanges` for a specific row
  private subscribeToWindowRowValueChanges(row: FormGroup, index: number): void {
    const subscription = row.valueChanges.subscribe(() => {
      this.onWindowRowValueChanged(index);
    });
    this.windowSubscriptions.push(subscription);
  }

  // Function executed whenever the values in a row change.
  private onWindowRowValueChanged(index: number): void {
    const row = this.windows.at(index) as FormGroup;

    if (row && row.valid && this.hasTriggeredWindowsValidation) {
      this.hasTriggeredWindowsValidation = false;
    }
  }

  // Subscribes to `valueChanges` for a specific row
  private subscribeToCustomRowValueChanges(row: FormGroup, index: number): void {
    const subscription = row.valueChanges.subscribe(() => {
      this.onCustomRowValueChanged(index);
    });
    this.customSubscriptions.push(subscription);
  }

  // Function executed whenever the values in a row change.
  private onCustomRowValueChanged(index: number): void {
    const row = this.customData.at(index);

    if (row && row.valid && this.hasTriggeredCustomValidation) {
      this.hasTriggeredCustomValidation = false;
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
    // List of keys to process
    const keysToProcess = ['leftTrim', 'rightTrim', 'upperTrim', 'belowThreshold'];

    // Filter valid rows and process their values
    return this.windows.controls
      .filter(row => row.valid) // Keep only valid rows
      .map(row => {
        // Create a copy of the row's value
        const processedRow = {...row.value};

        // Cast null or undefined values to 0 for specified keys
        for (const key of keysToProcess) {
          if (processedRow[key] === null || processedRow[key] === undefined) {
            processedRow[key] = 0;
          }
        }

        return processedRow;
      });
  }

  // Helper function to get valid rows from the FormArray
  private getValidCustomData(): any[] {
    return this.customData.controls
      .filter(row => row.valid)
      .map(row => {
        return {...row.value};
      });
  }

  // Build Billing Data payload for API
  private buildBillingPayload(): BillingPayload {
    return {
      supplierData: this.form.value.supplierData,
      customerData: this.form.value.customerData,
      ...this.buildPayload()
    };
  }

  // Build Data payload for API
  private buildPayload(): PricePayload {
    return {
      productData: {
        ...this.form.value.productData
      },
      ...this.buildWindowsPayload(),
      ...this.buildCustomPayload()
    };
  }

  // Build Windows payload for API using valid rows from the form
  private buildWindowsPayload(): WindowsPayload {
    const validRows = this.getValidWindowsData();
    return {
      windowsData: validRows
    };
  }

  // Build Custom payload for API using valid rows from the form
  private buildCustomPayload(): CustomPayload {
    const validRows = this.getValidCustomData();
    return {
      customData: validRows
    };
  }

  // Mark all fields as touched to show validation errors
  private markAllTouchedAndValidate(): void {
    this.form.markAllAsTouched();
    if (!this.areAllWindowRowsValid()) {
      this.hasTriggeredWindowsValidation = true;
    }
    if (!this.areAllCustomRowsValid()) {
      this.hasTriggeredCustomValidation = true;
    }
  }

  // Subscribe to changes in both the 'windows' FormArray and the 'productData' FormGroup
  private subscribeToFormChanges(): void {
    combineLatest([
      this.windows.valueChanges,
      this.customData.valueChanges.pipe(startWith(this.customData.value)),
      this.productData.valueChanges
    ]).pipe(debounceTime(300)).subscribe(() => {
      this.calculatePriceHandler();
    });
  }

  // Function to calculate the SHA-256 hash of the payload
  private calculateHash(payload: PricePayload): string {
    return CryptoJS.SHA256(JSON.stringify(payload)).toString();
  }

  private determineShowFillFormButton(): boolean {
    const isProdEnvironment = environment.production;

    if (isProdEnvironment) {
      return (window as any).variables?.showFillFormButton ?? false;
    } else {
      return environment.enableFillFormButton ?? false;
    }
  }

}

enum TabNames {
  supplier = 'supplier-tab',
  customer = 'personal-tab',
  product = 'product-tab',
  measurements = 'measurements-tab',
  customData = 'custom-data-tab'
}
