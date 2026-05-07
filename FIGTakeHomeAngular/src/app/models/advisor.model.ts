export interface BestMonth {
  year: number;
  month: number;
  commissionAmount: number;
}

export interface TopCarrier {
  carrierId: number;
  carrierName: string;
  totalCommissions: number;
}

export interface AdvisorSummary {
  advisorId: number;
  fullName: string;
  totalCommissions: number;
  statementCount: number;
  bestMonth: BestMonth;
  topCarrier: TopCarrier;
}

export interface AdvisorSummariesResponse {
  advisor1: AdvisorSummary;
  advisor2: AdvisorSummary;
  advisor3: AdvisorSummary;
}

export interface CarrierAllocation {
  carrierId: number;
  carrierName: string;
  totalCommissions: number;
  percentageOfTotal: number;
  statementCount: number;
  firstStatementDate: string;
  lastStatementDate: string;
  monthsSinceLastStatement: number;
  isGoneQuiet: boolean;
}

export interface CarrierDistribution {
  advisorId: number;
  advisorFullName: string;
  totalCommissions: number;
  carrierAllocations: CarrierAllocation[];
}

export interface Statement {
  id: number;
  advisorId: number;
  writingNumber: string | null;
  carrierId: number;
  premium: number;
  commissionAmount: number;
  statementDate: string;
  createdAt: string;
  updatedAt: string | null;
}

export interface MonthlyTrendEntry {
  label: string; // "Jan 2025"
  year: number;
  month: number;
  totalCommissions: number;
  statementCount: number;
}
