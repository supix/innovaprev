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
}

export interface InternalColor extends CollectionBaseItem {
}

export interface ExternalColor extends CollectionBaseItem {
}

export interface AccessoryColor extends CollectionBaseItem {
}

export interface ClimateZone extends CollectionBaseItem {
}

export interface WindowType extends CollectionBaseItem {
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
  internalColors: InternalColor[];
  externalColors: ExternalColor[];
  accessoryColors: AccessoryColor[];
  climateZones: ClimateZone[];
  windowTypes: WindowType[];
  openingTypes: OpeningType[];
  glassTypes: GlassType[];
  crosspieces: Crosspiece[];
}
