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

export interface EnergyOption {
  id: string;
  label: string;
  factor?: number | null;
}

export interface EnergyFuelOption {
  id: string;
  label: string;
  unit?: string | null;
  pci: number;
  pricePerUnit: number;
  priceLabel?: string | null;
  extraCoefficient?: number | null;
}

export interface EnergyDeductionOption {
  id: string;
  label: string;
  percentage: number;
  maxExpense: number;
  isApplicable: boolean;
}

export interface EnergyWindowOption {
  id: string;
  label: string;
  uw: number;
  unit?: string | null;
}

export interface EnergyRatioOption {
  id: string;
  label: string;
  ratio: number;
}

export interface EnergyMunicipality {
  id: string;
  comune: string;
  cap?: string | null;
  provincia?: string | null;
  regione?: string | null;
  altitudineSlm?: number | null;
  gradiGiorno?: number | null;
  zonaClimatica?: string | null;
  zonaVento?: string | null;
}

export interface EnergyCollectionsResponse {
  fuels: EnergyFuelOption[];
  deductions: EnergyDeductionOption[];
  buildingTypes: EnergyOption[];
  exposureTypes: EnergyOption[];
  oldFrameTypes: EnergyWindowOption[];
  oldGlassTypes: EnergyWindowOption[];
  frameAreaRatios: EnergyRatioOption[];
  permeabilityClasses: EnergyOption[];
  shadingOptions: EnergyOption[];
}

export interface EnergyCalculationResult {
  municipalityLabel: string;
  climateZone?: string | null;
  degreeDays: number;
  fuelLabel: string;
  buildingTypeLabel: string;
  exposureTypeLabel: string;
  windowSurfaceSqm: number;
  investmentAmount: number;
  oldWindowUw: number;
  newWindowUw: number;
  deltaUw: number;
  annualDispersionSavedKwh: number;
  annualPrimaryEnergySavedKwh: number;
  annualEconomicSaving: number;
  annualCo2SavedKg: number;
  deductionPercentage: number;
  deductionTotal: number;
  annualDeductionQuota: number;
  paybackYearsWithoutDeduction?: number | null;
  paybackYearsWithDeduction?: number | null;
}

// Base collection interface
export interface CollectionBaseItem {
  id: string;
  desc: string;
}

export interface Product extends CollectionBaseItem {
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
  frameTypeVisible: boolean;
  materialForProduct: string[];
}

export interface OpeningType extends CollectionBaseItem {
}

export interface GlassType extends CollectionBaseItem {
}

export interface FrameType extends CollectionBaseItem {
  frameForProduct: string[];
}

// Main Collections interface
export interface CollectionsResponse {
  product: Product[];
  colors: Colors[];
  accessoryColors: AccessoryColor[];
  windowTypes: WindowType[];
  openingTypes: OpeningType[];
  glassTypes: GlassType[];
  frameTypes: FrameType[];
}
