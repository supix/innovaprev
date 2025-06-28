export interface PersonalData {
  companyName: string;
  address: string;
  taxCode: string;
  phone: string;
  mail: string;
  iban: string;
}

export interface ProductDetails {
  orderNumber: string;
  product: string;
  glassStopper: boolean;
  windowSlide: boolean;
  internalColor: string;
  externalColor: string;
  accessoryColor: string;
  climateZone: string;
  notes: string;
}

export interface WindowRow {
  position: string;
  height: number;
  width: number;
  length: number;
  quantity: number;
  windowType: string;
  openingType: string;
  glassType: string;
  leftTrim: number;
  rightTrim: number;
  upperTrim: number;
  belowThreshold: number;
  wireCover: boolean;
}

export interface CustomRow {
  position: string;
  description: string;
  quantity: string;
  price: number;
}

export interface BillingPayload extends PricePayload {
  supplierData: PersonalData;
  customerData: PersonalData;
}

// Payload for a collection of window estimate rows
export interface PricePayload extends WindowsPayload, CustomPayload {
  productData: ProductDetails;
}

export interface WindowsPayload {
  windowsData: WindowRow[];
}

export interface CustomPayload {
  customData: CustomRow[];
}

export interface WindowInputBatch {
  position: string;
  height: number;
  width: number;
  materialType: string;
  wireCover?: boolean;
  openingType?: 'OT_DX' | 'OT_SX';
  glassType?: 'GT_TRASPARENTE' | 'GT_OPACO';
}
