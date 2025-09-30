import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgSelectConfig, NgSelectModule } from '@ng-select/ng-select';
import { ApiService } from '../../services/api.service';
import {
  BillingPayload,
  CollectionBaseItem,
  CollectionsResponse,
  Colors,
  CustomPayload,
  FrameType,
  PricePayload,
  Quotation,
  WindowInputBatch,
  WindowsPayload,
  WindowType
} from '../../models';
import {
  combineLatest,
  debounceTime,
  EMPTY,
  filter,
  finalize,
  forkJoin,
  of,
  startWith,
  Subject,
  Subscription,
  switchMap
} from 'rxjs';
import { PriceDisplayComponent } from '../price-display/price-display.component';
import {
  bankCoordinatesValidator,
  generateValidItalianVat,
  italianVatValidator,
  minNumber,
  phoneNumberValidator,
  validatorNumber
} from '../../validators/innova.validator';
import { catchError } from 'rxjs/operators';
import CryptoJS from 'crypto-js';
import { environment } from '../../../environments/environment';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { WindowApiService } from '../../services/window-api.service';
import { ModalService } from '../../services/modal.service';
import { SavedQuote, SavedQuotesService } from '../../services/saved-quotes.service';
import { ToastrService } from 'ngx-toastr';
import { LogoStorageService } from '../../services/logo-storage.service';

@Component({
  selector: 'app-innova-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgSelectModule, PriceDisplayComponent, FormsModule],
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

  submitted: boolean = false;
  hasTriggeredWindowsValidation: boolean = false;
  hasTriggeredCustomValidation: boolean = false;

  currentTab: TabNames = TabNames.supplier;
  tabNames = TabNames;

  showFillFormButton = false;
  showPreviewButton = false;

  externalColorList: Colors[] = [];
  internalColorList: Colors[] = [];
  windowTypeList: WindowType[] = [];
  frameTypeList: FrameType[] = [];

  yesNoOptions = [
    {label: 'Sì', value: true},
    {label: 'No', value: false}
  ];

  debugIndex: number = 1;

  quoteId: string | null = null;

  isLoading: boolean = false;
  private selectedProductId!: string | null;
  private calculatePriceSubject = new Subject<PricePayload | null>();
  private previousPayloadHash: string | null = null;
  private formChangesSub?: Subscription;
  private supplierDataFormChangesSub?: Subscription;
  private windowSubscriptions: Subscription[] = [];
  private customSubscriptions: Subscription[] = [];
  private drawableWindowTypes: string[] = [];

  private maxValues: { [key: string]: number } = {
    height: 5000,
    width: 5000,
    length: 5000,
    quantity: 500
  };

  constructor(private sanitizer: DomSanitizer, private fb: FormBuilder,
              private config: NgSelectConfig, private apiService: ApiService,
              private windowApiService: WindowApiService, private modalService: ModalService,
              private savedQuotesService: SavedQuotesService, private toastr: ToastrService,
              private logoStorage: LogoStorageService) {
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
    this.isCollectionsLoading = true;
    forkJoin({
      collections: this.apiService.getCollectionsData().pipe(
        catchError(() => of({
          product: [],
          colors: [],
          accessoryColors: [],
          windowTypes: [],
          openingTypes: [],
          glassTypes: [],
          frameTypes: []
        }))
      ),
      windowTypes: this.windowApiService.getDrawableWindowTypes().pipe(
        catchError(() => of([]))
      )
    })
      .pipe(
        finalize(() => this.isCollectionsLoading = false)
      )
      .subscribe(({collections, windowTypes}) => {
        this.collections = collections;
        this.drawableWindowTypes = windowTypes;
        this.showPreviewButton = Array.isArray(windowTypes) && windowTypes.length > 0;
        this.setupPriceCalculationSubscription();
        this.subscribeToFormChanges();
        this.subscribeToSupplierDataChanges();
        this.restoreCachedSupplierData();
        this.showFillFormButton = this.determineShowFillFormButton();
      });
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
    this.formChangesSub?.unsubscribe();
    this.supplierDataFormChangesSub?.unsubscribe();
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

  isFrameSectionVisible(): boolean {
    return !!this.frameTypeList.length;
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

  isPreviewWindowRowValid(index: number): boolean {
    const row = this.windows.at(index) as FormGroup;
    if (!row) return false;

    const validRow = this.isWindowRowValid(index);
    if (!validRow) return false;

    const windowType = row.get('windowType')?.value;

    if (!this.drawableWindowTypes || this.drawableWindowTypes.length === 0) {
      return false;
    }

    return this.drawableWindowTypes.includes(windowType);
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
    this.frameTypeList = this.getFrameTypes();
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
      .filter(type => type.materialForProduct.includes(this.selectedProductId as string))
      .sort((a, b) => a.id.localeCompare(b.id));
  }

  getFrameTypes(): FrameType[] {
    if (!this.selectedProductId || !this.collections?.frameTypes) {
      return [];
    }

    return this.collections.frameTypes
      .filter(type => type.frameForProduct.includes(this.selectedProductId as string));
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
        wireCover: [{value: null, disabled: true}, Validators.required],
        frameCode: [{value: null, disabled: true}, Validators.required]
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

  previewWindowRow(index: number): void {
    const row = this.windows.at(index) as FormGroup;
    if (!row || row.invalid) return;

    const input = row.value;
    const windowType = this.windowTypeList.find(type => type.id === input.windowType);

    this.windowApiService.drawWindow(input).subscribe({
      next: (blob) => this.modalService.showPreviewModal(windowType?.desc || '', blob),
    });
  }

  saveQuotes(): void {
    this.submitted = true;
    if (this.form.valid && this.quotation) {
      this.submitted = false;
      const payload: BillingPayload = this.buildBillingPayload();
      this.toastr.success(`Preventivo ${this.quoteId ? 'aggiornato' : 'salvato'} correttamente.`, 'Successo', {
        toastClass: 'custom-toastr ngx-toastr',
      });
      this.quoteId = this.savedQuotesService.upsert(this.quoteId, payload, this.quotation);
    } else {
      this.showWarningToastr();
      this.markAllTouchedAndValidate();
    }
  }

  async openModalArchive(): Promise<void> {
    const id = await this.modalService.showArchiveModal();
    if (!id) return;
    this.quoteId = id;
    const item = this.savedQuotesService.getById(id);
    if (!item) {
      console.warn('Preventivo non trovato!');
      return;
    }
    this.toastr.info('Preventivo caricato con successo.', 'Caricamento completato', {
      toastClass: 'custom-toastr ngx-toastr',
    });
    this.patchFormFromQuote(item);
  }

  openLogoManager(): void {
    this.modalService.showLogoManagerModal();
  }

  newQuote(): void {
    this.submitted = false;
    this.quoteId = null;
    this.onReset();
    this.restoreCachedSupplierData();
    this.toastr.info('Inserisci i dati per un nuovo preventivo.', 'Nuovo Preventivo', {
      toastClass: 'custom-toastr ngx-toastr'
    });
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
        a.remove();
        window.URL.revokeObjectURL(url);
        this.toastr.info('Download del preventivo completato.', 'Scaricato', {
          toastClass: 'custom-toastr ngx-toastr',
        });
      });
    } else {
      this.showWarningToastr();
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
      this.onChangeProduct(product);
    }

    const getRandomWindowType = (): string => {
      const windowTypeList = this.getWindowTypes();
      return windowTypeList?.length > 0 ? windowTypeList[Math.floor(Math.random() * windowTypeList.length)].id : 'defaultType';
    }

    const getRandomFrameType = (): string => {
      const frameTypes = this.getFrameTypes();
      return frameTypes?.length > 0 ? frameTypes[Math.floor(Math.random() * frameTypes.length)].id : 'defaultType';
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
      const windowType = getRandomWindowType();
      const {
        minAllowedHeight_mm,
        minAllowedLength_mm,
        minAllowedWidth_mm,
        maxAllowedHeight_mm,
        maxAllowedLength_mm,
        maxAllowedWidth_mm
      } = this.collections?.windowTypes.find(value => value.id === windowType as unknown as string) as WindowType || {};
      const row = this.fb.group({
        position: [0], // Updated later by updateWindowPositions
        height: [getRandomNumber(maxAllowedHeight_mm || this.maxValues['height'], minAllowedHeight_mm || 500)],
        width: [getRandomNumber(maxAllowedWidth_mm || this.maxValues['width'], minAllowedWidth_mm || 500)],
        length: [getRandomNumber(maxAllowedLength_mm || this.maxValues['length'], minAllowedLength_mm || 500)],
        quantity: [getRandomNumber(5)],
        windowType: [windowType, Validators.required],
        openingType: [getRandomItem(this.collections.openingTypes, 'defaultOpening'), Validators.required],
        glassType: [getRandomItem(this.collections.glassTypes, 'defaultGlass'), Validators.required],
        wireCover: [getRandomBoolean(), Validators.required],
        frameCode: [getRandomFrameType(), Validators.required]
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

    // Helper function to randomly return true or false
    function getRandomBoolean(): boolean {
      return Math.random() < 0.5;
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

  private showWarningToastr(): void {
    const problems: string[] = [];
    if (this.hasErrors(this.supplierData)) problems.push('Fornitore');
    if (this.hasErrors(this.customerData)) problems.push('Cliente');
    if (this.hasErrors(this.productData)) problems.push('Serie');
    if (!this.areAllWindowRowsValid()) problems.push('Misure');
    if (!this.areAllCustomRowsValid()) problems.push('Componenti aggiuntivi');

    const detail = problems.length ? ` Sezioni non valide: ${problems.join(', ')}.` : '';

    this.toastr.warning('Alcuni campi del preventivo non sono stati compilati.' + detail, 'Attenzione', {
      toastClass: 'custom-toastr ngx-toastr',
    });
  }

  private patchFormFromQuote(item: SavedQuote): void {
    if (!this.collections) {
      console.error('Collections non caricate');
      return;
    }

    const { billingPayload } = item;

    const product = billingPayload.productData?.product;
    const productExists = !!this.collections.product.find(p => p.id === product);

    if (!productExists) {
      console.warn('Prodotto non più disponibile:', product);
      return;
    }

    this.onReset();

    this.onChangeProduct(product);

    this.form.patchValue({
      supplierData: billingPayload.supplierData,
      customerData: billingPayload.customerData,
      productData: billingPayload.productData
    });
    this.windows.removeAt(0);

    billingPayload.windowsData.forEach( (win, i) => {
        const row = this.fb.group({
          position: [win.position],
          height: [win.height],
          width: [win.width],
          length: [win.length],
          quantity: [win.quantity],
          windowType: [win.windowType],
          openingType: [win.openingType],
          glassType: [win.glassType],
          wireCover: [win.wireCover],
          frameCode: [win.frameCode]
        });
        this.windows.push(row);
        this.subscribeToWindowRowValueChanges(row, i);
    });

    this.updateWindowPositions();


    billingPayload.customData.forEach((comp, i) => {
      const row = this.fb.group({
        position: [comp.position],
        quantity: [comp.quantity],
        description: [comp.description],
        price: [comp.price]
      });
      this.customData.push(row);
      this.subscribeToCustomRowValueChanges(row, i);
    });
    this.updateCustomPositions();

    this.hasTriggeredWindowsValidation = false;
    this.hasTriggeredCustomValidation = false;
  }


  private fillSupplierForm(data: any): void {
    this.supplierData.patchValue({
      companyName: data.companyName || '',
      address: data.address || '',
      taxCode: data.taxCode || '',
      phone: data.phone || '',
      mail: data.mail || '',
      iban: data.iban || ''
    });
  }

  private restoreCachedSupplierData(): void {
    const cached = localStorage.getItem('cachedSupplierData');
    if (cached) {
      const data = JSON.parse(cached);
      this.fillSupplierForm(data);
    }
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

      // Apply the same validation/enable/disable logic for frameCode based on frameTypeList
      const frameCodeControl = windowGroup.get('frameCode');
      if (frameCodeControl) {
        const selectedFrameCode = frameCodeControl.value;

        if (this.frameTypeList.length === 0) {
          frameCodeControl.disable();
          frameCodeControl.setValue(null, {emitEvent: false});
          frameCodeControl.clearValidators();
        } else {
          const windowTypeControl = windowGroup.get('windowType');
          if (windowTypeControl) {
            const selectedWindowType = windowTypeControl.value;
            const {
              frameTypeVisible
            } = this.collections?.windowTypes.find(value => value.id === selectedWindowType as unknown as string) as WindowType || {};
            const isValidFrameCode = this.frameTypeList.some((type: FrameType) => type.id === selectedFrameCode);

            if (!frameTypeVisible || this.frameTypeList.length === 0) {
              frameCodeControl.disable();
              frameCodeControl.setValue(null, {emitEvent: false});
              frameCodeControl.clearValidators();
            } else {
              if (!isValidFrameCode) {
                frameCodeControl.setValue(null, {emitEvent: false});
              }
              frameCodeControl.enable();
              frameCodeControl.setValidators([Validators.required]);
            }
          } else {
            frameCodeControl.disable();
            frameCodeControl.setValue(null, {emitEvent: false});
            frameCodeControl.clearValidators();
          }
        }

        frameCodeControl.updateValueAndValidity({emitEvent: false});
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

  private onWindowRowValueChanged(index: number): void {
    const row = this.windows.at(index) as FormGroup;

    if (row) {
      const windowType = row.get('windowType')?.value;

      if (windowType) {
        const {
          numOfDims,
          openingTypeVisible,
          glassTypeVisible,
          wireCoverVisible,
          frameTypeVisible,
          minAllowedHeight_mm,
          minAllowedLength_mm,
          minAllowedWidth_mm,
          maxAllowedHeight_mm,
          maxAllowedLength_mm,
          maxAllowedWidth_mm
        } = this.collections?.windowTypes.find(value => value.id === windowType as unknown as string) as WindowType || {};

        // Opening Type
        if (openingTypeVisible) {
          row.get('openingType')?.setValidators([Validators.required]);
          row.get('openingType')?.enable({emitEvent: false});
        } else {
          row.get('openingType')?.setValidators(null);
          row.get('openingType')?.disable({emitEvent: false});
          row.patchValue({openingType: null}, {emitEvent: false});
        }

        // Glass Type
        if (glassTypeVisible) {
          row.get('glassType')?.setValidators([Validators.required]);
          row.get('glassType')?.enable({emitEvent: false});
        } else {
          row.get('glassType')?.setValidators(null);
          row.get('glassType')?.disable({emitEvent: false});
          row.patchValue({glassType: null}, {emitEvent: false});
        }

        // Frame Code
        if (frameTypeVisible && this.frameTypeList.length > 0) {
          row.get('frameCode')?.setValidators([Validators.required]);
          row.get('frameCode')?.enable({emitEvent: false});
        } else {
          row.get('frameCode')?.setValidators(null);
          row.get('frameCode')?.disable({emitEvent: false});
          row.patchValue({frameCode: null}, {emitEvent: false});
        }

        // Wire Cover
        if (wireCoverVisible) {
          row.get('wireCover')?.setValidators([Validators.required]);
          row.get('wireCover')?.enable({emitEvent: false});
        } else {
          row.get('wireCover')?.setValidators(null);
          row.get('wireCover')?.disable({emitEvent: false});
          row.patchValue({wireCover: null}, {emitEvent: false});
        }

        // Dimension Fields
        if (numOfDims === 2) {
          row.get('height')?.setValidators([
            validatorNumber({
              required: true,
              min: minAllowedHeight_mm || 1,
              max: maxAllowedHeight_mm,
              gender: 'f'
            })
          ]);

          row.get('width')?.setValidators([
            validatorNumber({
              required: true,
              min: minAllowedWidth_mm || 1,
              max: maxAllowedWidth_mm,
              gender: 'f'
            })
          ]);

          row.get('length')?.setValidators(null);

          row.get('height')?.enable({emitEvent: false});
          row.get('width')?.enable({emitEvent: false});
          row.get('length')?.disable({emitEvent: false});

          row.patchValue({length: null}, {emitEvent: false});

        } else if (numOfDims === 1) {
          row.get('height')?.setValidators(null);
          row.get('width')?.setValidators(null);

          row.get('length')?.setValidators([
            validatorNumber({
              required: true,
              min: minAllowedLength_mm || 1,
              max: maxAllowedLength_mm,
              gender: 'f'
            })
          ]);

          row.get('height')?.disable({emitEvent: false});
          row.get('width')?.disable({emitEvent: false});
          row.get('length')?.enable({emitEvent: false});

          row.patchValue({height: null, width: null}, {emitEvent: false});
        }

        // Update validity
        row.get('height')?.updateValueAndValidity({emitEvent: false});
        row.get('width')?.updateValueAndValidity({emitEvent: false});
        row.get('length')?.updateValueAndValidity({emitEvent: false});
        row.get('glassType')?.updateValueAndValidity({emitEvent: false});
        row.get('wireCover')?.updateValueAndValidity({emitEvent: false});
        row.get('openingType')?.updateValueAndValidity({emitEvent: false});
        row.get('frameCode')?.updateValueAndValidity({emitEvent: false});
      }

      if (row.valid && this.hasTriggeredWindowsValidation) {
        this.hasTriggeredWindowsValidation = false;
      }
    }
  }

  getWindowInputPayload(): WindowInputBatch[] {
    if (!this.form.valid) return [];
    const windowsPayload: WindowsPayload = this.buildWindowsPayload();
    if (!windowsPayload?.windowsData?.length) return [];

    return windowsPayload.windowsData
      .filter(w => this.drawableWindowTypes.includes(w.windowType))
      .map(w => ({
        position: '' + w.position,
        height: w.height,
        width: w.width,
        wireCover: w.wireCover,
        materialType: w.windowType,
        openingType: w.openingType as "OT_DX" | "OT_SX" | undefined,
        glassType: w.glassType as "GT_TRASPARENTE" | "GT_OPACO" | undefined
      }));
  }

  debugDownloadZip(): void {
    if (!this.form.valid) return;
    this.isLoading = true;
    const payload: WindowInputBatch[] = this.getWindowInputPayload();
    if (!payload.length) {
      this.isLoading = false;
      return;
    }

    this.windowApiService.drawWindowsBatch(payload).pipe(
      finalize(() => this.isLoading = false)
    ).subscribe((response) => {
      const blob = new Blob([response], {type: 'application/zip'});
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `windows-${Date.now()}.zip`;
      document.body.appendChild(a);
      a.click();
      a.remove();
      window.URL.revokeObjectURL(url);
    });
  }

  debugWindowRowValidators(index: number): void {
    const row = this.windows.at(index - 1) as FormGroup;

    if (!row) {
      console.warn(`No row found at index ${index}.`);
      return;
    }

    const fields = ['height', 'width', 'length', 'glassType', 'wireCover', 'openingType', 'frameCode'];

    console.group(`Active validators for row ${index}`);

    fields.forEach(field => {
      const control = row.get(field);

      if (!control) {
        console.log(`${field}: control not found`);
        return;
      }

      const rawValidators = (control as any)._rawValidators || [];
      const isEnabled = !control.disabled;
      const hasErrors = control.errors;

      console.groupCollapsed(`${field} (${isEnabled ? 'enabled' : 'disabled'})`);

      rawValidators.forEach((validator: any, idx: number) => {
        const meta = validator._validatorInfo;
        if (meta) {
          console.log(`Validator #${idx}:`, meta);
        } else {
          console.log(`Validator #${idx}: (no metadata available)`, validator.toString());
        }
      });

      console.log('Validation errors:', hasErrors);
      console.groupEnd();
    });

    console.groupEnd();
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

    // Filter valid rows and process their values
    return this.windows.controls
      .filter(row => row.valid) // Keep only valid rows
      .map(row => {
        // Create a copy of the row's value
        return {...row.value};
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
    const activeLogo = this.logoStorage.list().find(l => l.active)?.dataUrl;
    return {
      supplierData: this.form.value.supplierData,
      customerData: this.form.value.customerData,
      ...this.buildPayload(),
      logoDataUrl: activeLogo
    };
  }

  // Build Data payload for API
  private buildPayload(): PricePayload {
    return {
      productData: {
        ...this.form.get('productData')?.getRawValue()
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
    this.formChangesSub = combineLatest([
      this.windows.valueChanges,
      this.customData.valueChanges.pipe(startWith(this.customData.value)),
      this.productData.valueChanges
    ]).pipe(debounceTime(300)).subscribe(() => {
      this.calculatePriceHandler();
    });
  }

  private subscribeToSupplierDataChanges(): void {
    this.supplierDataFormChangesSub = this.supplierData.valueChanges
      .pipe(debounceTime(300), startWith(this.supplierData.value))
      .subscribe(data => {
        const hasSomeData = Object.values(data).some(v => !!v);
        if (hasSomeData) {
          localStorage.setItem('cachedSupplierData', JSON.stringify(data));
        }
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
