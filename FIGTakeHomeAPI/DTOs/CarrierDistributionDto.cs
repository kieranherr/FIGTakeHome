namespace FIGTakeHomeAPI.DTOs
{
    public class CarrierDistributionDto
    {
        public int AdvisorId { get; set; }
        public string AdvisorFullName { get; set; } = string.Empty;
        public decimal TotalCommissions { get; set; }
        public List<CarrierAllocationDto> CarrierAllocations { get; set; } = [];
    }

    public class CarrierAllocationDto
    {
        public int CarrierId { get; set; }
        public string CarrierName { get; set; } = string.Empty;
        public decimal TotalCommissions { get; set; }
        public decimal PercentageOfTotal { get; set; }
        public int StatementCount { get; set; }
        public DateTime FirstStatementDate { get; set; }
        public DateTime LastStatementDate { get; set; }
        public int MonthsSinceLastStatement { get; set; }
        public bool IsGoneQuiet { get; set; }
    }
}
