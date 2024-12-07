export interface PersonalData {
  companyName: string;
  address: string;
  vat: string;
  phone: string;
  mail: string;
  orderNumber: string;
}

export interface ProductDetails {
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
  quantity: number;
  windowType: string;
  openingType: string;
  glassType: string;
  crosspiece: string;
  leftTrim: number;
  rightTrim: number;
  upperTrim: number;
  belowThreshold: number;
}


// Payload for a collection of window estimate rows
export interface DataPayload {
  personalData: PersonalData;
  productData: ProductDetails;
  windowsData: WindowRow[];
}
