using FIGTakeHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FIGTakeHomeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Advisor> Advisors => Set<Advisor>();
        public DbSet<Carrier> Carriers => Set<Carrier>();
        public DbSet<Statement> Statements => Set<Statement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Statement>()
                .HasOne(s => s.Advisor)
                .WithMany()
                .HasForeignKey(s => s.AdvisorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Statement>()
                .HasOne(s => s.Carrier)
                .WithMany()
                .HasForeignKey(s => s.CarrierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Advisor>()
                .HasOne(a => a.Carrier)
                .WithMany()
                .HasForeignKey(a => a.CarrierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Carrier>().HasData(
                new Carrier
                {
                    Id = 1,
                    CarrierName = "Nationwide",
                    AMBestRating = "A+",
                    NAICCode = "23787",
                    PhoneNumber = "800-421-3535",
                    Website = "https://www.nationwide.com",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Carrier
                {
                    Id = 2,
                    CarrierName = "Prudential Financial",
                    AMBestRating = "A+",
                    NAICCode = "68241",
                    PhoneNumber = "800-778-2255",
                    Website = "https://www.prudential.com",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Carrier
                {
                    Id = 3,
                    CarrierName = "Lincoln Financial Group",
                    AMBestRating = "A",
                    NAICCode = "65676",
                    PhoneNumber = "877-275-5462",
                    Website = "https://www.lfg.com",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Carrier
                {
                    Id = 4,
                    CarrierName = "Pacific Life",
                    AMBestRating = "A+",
                    NAICCode = "67466",
                    PhoneNumber = "800-800-7681",
                    Website = "https://www.pacificlife.com",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Advisor>().HasData(
                new Advisor
                {
                    Id = 1,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "555-123-4567",
                    NPN = "NPN1234567",
                    LicenseNumber = "LIC-001",
                    LicenseState = "TX",
                    LicenseExpirationDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                    CarrierId = 1,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Advisor
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "555-987-6543",
                    NPN = "NPN7654321",
                    LicenseNumber = "LIC-002",
                    LicenseState = "CA",
                    LicenseExpirationDate = new DateTime(2026, 6, 30, 0, 0, 0, DateTimeKind.Utc),
                    CarrierId = 2,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Advisor
                {
                    Id = 3,
                    FirstName = "Maria",
                    LastName = "Garcia",
                    Email = "maria.garcia@example.com",
                    PhoneNumber = "555-456-7890",
                    NPN = "NPN9876543",
                    LicenseNumber = "LIC-003",
                    LicenseState = "FL",
                    LicenseExpirationDate = new DateTime(2027, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                    CarrierId = 3,
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            // Statements span Jan 2025 – Dec 2025 (first 3 pairings) and Jan 2026 – Dec 2026 (last 3 pairings)
            var statements = new List<Statement>();
            int id = 1;

            var advisorCarriers = new[]
            {
                (AdvisorId: 1, CarrierId: 1, WritingPrefix: "WN-A1", BasePremium: 5000m,  CommissionRate: 0.05m,  Year: 2025),
                (AdvisorId: 1, CarrierId: 2, WritingPrefix: "WN-A2", BasePremium: 7500m,  CommissionRate: 0.06m,  Year: 2025),
                (AdvisorId: 2, CarrierId: 2, WritingPrefix: "WN-B1", BasePremium: 12000m, CommissionRate: 0.06m,  Year: 2025),
                (AdvisorId: 2, CarrierId: 3, WritingPrefix: "WN-B2", BasePremium: 8000m,  CommissionRate: 0.05m,  Year: 2026),
                (AdvisorId: 3, CarrierId: 3, WritingPrefix: "WN-C1", BasePremium: 9500m,  CommissionRate: 0.055m, Year: 2026),
                (AdvisorId: 3, CarrierId: 4, WritingPrefix: "WN-C2", BasePremium: 11000m, CommissionRate: 0.06m,  Year: 2026),
            };

            foreach (var (advisorId, carrierId, writingPrefix, basePremium, commissionRate, year) in advisorCarriers)
            {
                for (int month = 1; month <= 12; month++)
                {
                    var statementDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
                    var premium = basePremium + (month * 100m);
                    statements.Add(new Statement
                    {
                        Id = id++,
                        AdvisorId = advisorId,
                        CarrierId = carrierId,
                        WritingNumber = $"{writingPrefix}-{month:D2}",
                        Premium = premium,
                        CommissionAmount = Math.Round(premium * commissionRate, 2),
                        StatementDate = statementDate,
                        CreatedAt = statementDate
                    });
                }
            }

            modelBuilder.Entity<Statement>().HasData(statements);
        }
    }
}
