export interface VatCalculationRequest {
  vatRate: number;
  netAmount?: number;
  grossAmount?: number;
  vatAmount?: number;
}
