// Represents the details of a quotation
export interface Quotation {
  amount: number;
  tax: number;
  grandTotal: number;
}

// Represents the response structure containing a quotation
export interface QuotationResponse {
  quotation: Quotation;
}

// Base collection interface
export interface CollectionBaseItem {
  id: string;
  desc: string;
}

export interface Product extends CollectionBaseItem {
  trimSectionVisible: boolean;
  extDesc: string;
  descTitle: string;
  singleColor: boolean;
}

export interface Colors extends CollectionBaseItem {
  internalColorForProduct: string[];
  externalColorForProduct: string[];
}

export interface AccessoryColor extends CollectionBaseItem {
}

export interface WindowType extends CollectionBaseItem {
  numOfDims: number;
  minAllowedHeight_mm?: number;
  minAllowedLength_mm?: number;
  minAllowedWidth_mm?: number;
  maxAllowedHeight_mm?: number;
  maxAllowedLength_mm?: number;
  maxAllowedWidth_mm?: number;
  openingTypeVisible: boolean;
  glassTypeVisible: boolean;
  wireCoverVisible: boolean;
  materialForProduct: string[];
}

export interface OpeningType extends CollectionBaseItem {
}

export interface GlassType extends CollectionBaseItem {
}

export interface Crosspiece extends CollectionBaseItem {
}

// Main Collections interface
export interface CollectionsResponse {
  product: Product[];
  colors: Colors[];
  accessoryColors: AccessoryColor[];
  windowTypes: WindowType[];
  openingTypes: OpeningType[];
  glassTypes: GlassType[];
  crosspieces: Crosspiece[];
}
