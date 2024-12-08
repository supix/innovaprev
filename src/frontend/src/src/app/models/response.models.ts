// Represents the details of a quotation
export interface Quotation {
  amount: number;
}

// Represents the response structure containing a quotation
export interface QuotationResponse {
  quotation: Quotation;
}
