using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FIGTakeHomeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AMBestRating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NAICCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Advisors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LicenseState = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LicenseExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NPN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advisors_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    WritingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    Premium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statements_Advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statements_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Carriers",
                columns: new[] { "Id", "AMBestRating", "Address", "CarrierName", "City", "CreatedAt", "IsActive", "NAICCode", "PhoneNumber", "State", "UpdatedAt", "Website", "ZipCode" },
                values: new object[,]
                {
                    { 1, "A+", null, "Nationwide", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "23787", "800-421-3535", null, null, "https://www.nationwide.com", null },
                    { 2, "A+", null, "Prudential Financial", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "68241", "800-778-2255", null, null, "https://www.prudential.com", null },
                    { 3, "A", null, "Lincoln Financial Group", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "65676", "877-275-5462", null, null, "https://www.lfg.com", null },
                    { 4, "A+", null, "Pacific Life", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, "67466", "800-800-7681", null, null, "https://www.pacificlife.com", null }
                });

            migrationBuilder.InsertData(
                table: "Advisors",
                columns: new[] { "Id", "Address", "CarrierId", "City", "CreatedAt", "Email", "FirstName", "IsActive", "LastName", "LicenseExpirationDate", "LicenseNumber", "LicenseState", "NPN", "PhoneNumber", "State", "UpdatedAt", "ZipCode" },
                values: new object[,]
                {
                    { 1, null, 1, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "jane.smith@example.com", "Jane", true, "Smith", new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "LIC-001", "TX", "NPN1234567", "555-123-4567", null, null, null },
                    { 2, null, 2, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "john.doe@example.com", "John", true, "Doe", new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), "LIC-002", "CA", "NPN7654321", "555-987-6543", null, null, null },
                    { 3, null, 3, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "maria.garcia@example.com", "Maria", true, "Garcia", new DateTime(2027, 3, 31, 0, 0, 0, 0, DateTimeKind.Utc), "LIC-003", "FL", "NPN9876543", "555-456-7890", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Statements",
                columns: new[] { "Id", "AdvisorId", "CarrierId", "CommissionAmount", "CreatedAt", "Premium", "StatementDate", "UpdatedAt", "WritingNumber" },
                values: new object[,]
                {
                    { 1, 1, 1, 255.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5100m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-01" },
                    { 2, 1, 1, 260.00m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5200m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-02" },
                    { 3, 1, 1, 265.00m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5300m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-03" },
                    { 4, 1, 1, 270.00m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5400m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-04" },
                    { 5, 1, 1, 275.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5500m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-05" },
                    { 6, 1, 1, 280.00m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5600m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-06" },
                    { 7, 1, 1, 285.00m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5700m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-07" },
                    { 8, 1, 1, 290.00m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5800m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-08" },
                    { 9, 1, 1, 295.00m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5900m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-09" },
                    { 10, 1, 1, 300.00m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-10" },
                    { 11, 1, 1, 305.00m, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6100m, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-11" },
                    { 12, 1, 1, 310.00m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6200m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A1-12" },
                    { 13, 1, 2, 456.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7600m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-01" },
                    { 14, 1, 2, 462.00m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7700m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-02" },
                    { 15, 1, 2, 468.00m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7800m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-03" },
                    { 16, 1, 2, 474.00m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7900m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-04" },
                    { 17, 1, 2, 480.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8000m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-05" },
                    { 18, 1, 2, 486.00m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8100m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-06" },
                    { 19, 1, 2, 492.00m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8200m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-07" },
                    { 20, 1, 2, 498.00m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8300m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-08" },
                    { 21, 1, 2, 504.00m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8400m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-09" },
                    { 22, 1, 2, 510.00m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8500m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-10" },
                    { 23, 1, 2, 516.00m, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8600m, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-11" },
                    { 24, 1, 2, 522.00m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8700m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-A2-12" },
                    { 25, 2, 2, 726.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12100m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-01" },
                    { 26, 2, 2, 732.00m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12200m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-02" },
                    { 27, 2, 2, 738.00m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12300m, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-03" },
                    { 28, 2, 2, 744.00m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12400m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-04" },
                    { 29, 2, 2, 750.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12500m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-05" },
                    { 30, 2, 2, 756.00m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12600m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-06" },
                    { 31, 2, 2, 762.00m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12700m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-07" },
                    { 32, 2, 2, 768.00m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12800m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-08" },
                    { 33, 2, 2, 774.00m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12900m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-09" },
                    { 34, 2, 2, 780.00m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13000m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-10" },
                    { 35, 2, 2, 786.00m, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13100m, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-11" },
                    { 36, 2, 2, 792.00m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13200m, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B1-12" },
                    { 37, 2, 3, 405.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8100m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-01" },
                    { 38, 2, 3, 410.00m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8200m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-02" },
                    { 39, 2, 3, 415.00m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8300m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-03" },
                    { 40, 2, 3, 420.00m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8400m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-04" },
                    { 41, 2, 3, 425.00m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8500m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-05" },
                    { 42, 2, 3, 430.00m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8600m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-06" },
                    { 43, 2, 3, 435.00m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8700m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-07" },
                    { 44, 2, 3, 440.00m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8800m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-08" },
                    { 45, 2, 3, 445.00m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8900m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-09" },
                    { 46, 2, 3, 450.00m, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9000m, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-10" },
                    { 47, 2, 3, 455.00m, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9100m, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-11" },
                    { 48, 2, 3, 460.00m, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9200m, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-B2-12" },
                    { 49, 3, 3, 528.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9600m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-01" },
                    { 50, 3, 3, 533.50m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9700m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-02" },
                    { 51, 3, 3, 539.00m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9800m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-03" },
                    { 52, 3, 3, 544.50m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9900m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-04" },
                    { 53, 3, 3, 550.00m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10000m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-05" },
                    { 54, 3, 3, 555.50m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10100m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-06" },
                    { 55, 3, 3, 561.00m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10200m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-07" },
                    { 56, 3, 3, 566.50m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10300m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-08" },
                    { 57, 3, 3, 572.00m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10400m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-09" },
                    { 58, 3, 3, 577.50m, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10500m, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-10" },
                    { 59, 3, 3, 583.00m, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10600m, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-11" },
                    { 60, 3, 3, 588.50m, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10700m, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C1-12" },
                    { 61, 3, 4, 666.00m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11100m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-01" },
                    { 62, 3, 4, 672.00m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11200m, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-02" },
                    { 63, 3, 4, 678.00m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11300m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-03" },
                    { 64, 3, 4, 684.00m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11400m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-04" },
                    { 65, 3, 4, 690.00m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11500m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-05" },
                    { 66, 3, 4, 696.00m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11600m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-06" },
                    { 67, 3, 4, 702.00m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11700m, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-07" },
                    { 68, 3, 4, 708.00m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11800m, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-08" },
                    { 69, 3, 4, 714.00m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11900m, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-09" },
                    { 70, 3, 4, 720.00m, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12000m, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-10" },
                    { 71, 3, 4, 726.00m, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12100m, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-11" },
                    { 72, 3, 4, 732.00m, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12200m, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "WN-C2-12" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advisors_CarrierId",
                table: "Advisors",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_AdvisorId",
                table: "Statements",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Statements_CarrierId",
                table: "Statements",
                column: "CarrierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "Advisors");

            migrationBuilder.DropTable(
                name: "Carriers");
        }
    }
}
