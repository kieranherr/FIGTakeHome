namespace FIGTakeHomeAPI.DTOs
{
    public class AdvisorSummaryDto
    {
        public int AdvisorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public decimal TotalCommissions { get; set; }
        public int StatementCount { get; set; }
        public BestMonthDto BestMonth { get; set; } = new();
        public TopCarrierDto TopCarrier { get; set; } = new();
    }

    public class AdvisorSummariesResponseDto
    {
        public AdvisorSummaryDto Advisor1 { get; set; } = new();
        public AdvisorSummaryDto Advisor2 { get; set; } = new();
        public AdvisorSummaryDto Advisor3 { get; set; } = new();
    }

    public class BestMonthDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal CommissionAmount { get; set; }
    }

    public class TopCarrierDto
    {
        public int CarrierId { get; set; }
        public string CarrierName { get; set; } = string.Empty;
        public decimal TotalCommissions { get; set; }
    }
}