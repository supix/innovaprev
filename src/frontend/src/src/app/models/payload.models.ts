// Payload for personal data
export interface PersonalDataPayload {
  firstName: string;
  lastName: string;
  address: string;
}

// Payload for billing data
export interface BillingDataPayload {
  vatNumber: string;
  taxCode: string;
  billingAddress: string;
}

// Payload for a single window estimate row
export interface WindowEstimateRowPayload {
  height: number;
  width: number;
  color: string;
  quantity: number;
}

// Payload for a collection of window estimate rows
export interface DataPayload {
  personalData: PersonalDataPayload;
  billingData: BillingDataPayload;
  windows: WindowEstimateRowPayload[];
}
