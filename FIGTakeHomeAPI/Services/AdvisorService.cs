using FIGTakeHomeAPI.Data;
using FIGTakeHomeAPI.DTOs;
using FIGTakeHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIGTakeHomeAPI.Services
{
    public class AdvisorService
    {
        private readonly AppDbContext _context;

        public AdvisorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdvisorSummariesResponseDto> GetAdvisorSummariesAsync()
        {
            var advisors = await _context.Advisors
                .Where(a => a.IsActive)
                .ToListAsync();

            var statements = await _context.Statements
                .Include(s => s.Carrier)
                .ToListAsync();

            var summaries = advisors.Select(advisor =>
            {
                var advisorStatements = statements
                    .Where(s => s.AdvisorId == advisor.Id)
                    .ToList();

                var totalCommissions = advisorStatements.Sum(s => s.CommissionAmount);
                var statementCount = advisorStatements.Count;

                var bestMonth = advisorStatements
                    .GroupBy(s => new { s.StatementDate.Year, s.StatementDate.Month })
                    .Select(g => new BestMonthDto
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        CommissionAmount = g.Sum(s => s.CommissionAmount)
                    })
                    .OrderByDescending(m => m.CommissionAmount)
                    .FirstOrDefault() ?? new BestMonthDto();

                var topCarrier = advisorStatements
                    .GroupBy(s => new { s.CarrierId, Name = s.Carrier?.CarrierName ?? string.Empty })
                    .Select(g => new TopCarrierDto
                    {
                        CarrierId = g.Key.CarrierId,
                        CarrierName = g.Key.Name,
                        TotalCommissions = g.Sum(s => s.CommissionAmount)
                    })
                    .OrderByDescending(c => c.TotalCommissions)
                    .FirstOrDefault() ?? new TopCarrierDto();

                return new AdvisorSummaryDto
                {
                    AdvisorId = advisor.Id,
                    FullName = $"{advisor.FirstName} {advisor.LastName}",
                    TotalCommissions = totalCommissions,
                    StatementCount = statementCount,
                    BestMonth = bestMonth,
                    TopCarrier = topCarrier
                };
            }).ToList();

            return new AdvisorSummariesResponseDto
            {
                Advisor1 = summaries.ElementAtOrDefault(0) ?? new AdvisorSummaryDto(),
                Advisor2 = summaries.ElementAtOrDefault(1) ?? new AdvisorSummaryDto(),
                Advisor3 = summaries.ElementAtOrDefault(2) ?? new AdvisorSummaryDto()
            };
        }

        public async Task<CarrierDistributionDto> GetCarrierDistributionAsync(int advisorId)
        {
            var advisor = await _context.Advisors
                .Where(a => a.IsActive && a.Id == advisorId)
                .FirstOrDefaultAsync();

            if (advisor is null)
                return new CarrierDistributionDto();

            var statements = await _context.Statements
                .Include(s => s.Carrier)
                .Where(s => s.AdvisorId == advisorId)
                .ToListAsync();

            var totalCommissions = statements.Sum(s => s.CommissionAmount);
            var now = DateTime.UtcNow;
            // A carrier is considered "gone quiet" if no statement has been received in the last 3 months
            var quietThresholdMonths = 3;

            var allocations = statements
                .GroupBy(s => new { s.CarrierId, Name = s.Carrier?.CarrierName ?? string.Empty })
                .Select(g =>
                {
                    var carrierTotal = g.Sum(s => s.CommissionAmount);
                    var lastStatement = g.Max(s => s.StatementDate);
                    var monthsSinceLast = ((now.Year - lastStatement.Year) * 12) + (now.Month - lastStatement.Month);

                    return new CarrierAllocationDto
                    {
                        CarrierId = g.Key.CarrierId,
                        CarrierName = g.Key.Name,
                        TotalCommissions = carrierTotal,
                        PercentageOfTotal = totalCommissions > 0
                            ? Math.Round(carrierTotal / totalCommissions * 100, 2)
                            : 0,
                        StatementCount = g.Count(),
                        FirstStatementDate = g.Min(s => s.StatementDate),
                        LastStatementDate = lastStatement,
                        MonthsSinceLastStatement = monthsSinceLast,
                        IsGoneQuiet = monthsSinceLast >= quietThresholdMonths
                    };
                })
                .OrderByDescending(a => a.TotalCommissions)
                .ToList();

            return new CarrierDistributionDto
            {
                AdvisorId = advisor.Id,
                AdvisorFullName = $"{advisor.FirstName} {advisor.LastName}",
                TotalCommissions = totalCommissions,
                CarrierAllocations = allocations
            };
        }

        public async Task<IEnumerable<Statement>> GetAdvisorMonthlyTrend(int advisorId)
        {
            var advisor = await _context.Advisors
                .Where(a => a.IsActive && a.Id == advisorId)
                .ToListAsync();
            var now = DateTime.UtcNow;
            var currentQuarterNumber = (now.Month - 1) / 3 + 1;
            var currentQuarterStartMonth = new DateTime(now.Year, (currentQuarterNumber - 1) * 3 + 1, 1, 0, 0, 0, DateTimeKind.Utc).Month;
            var currentQuarterEndMonth = currentQuarterStartMonth + 2;

            var statements = await _context.Statements
                .Include(s => s.Carrier)
                .Where(x => x.AdvisorId == advisorId && 
                                (x.CreatedAt.Year == now.Year && 
                                 x.CreatedAt.Month >= currentQuarterStartMonth && 
                                 x .CreatedAt.Month <= currentQuarterEndMonth))
                //.Select(x => new Statement { 
                //    Id = x.Id,
                //    StatementDate = x.StatementDate,
                //    CreatedAt = x.CreatedAt,
                //    AdvisorId = x.AdvisorId
                //})
                .ToListAsync();
            return statements;
            
        }
    }
}
