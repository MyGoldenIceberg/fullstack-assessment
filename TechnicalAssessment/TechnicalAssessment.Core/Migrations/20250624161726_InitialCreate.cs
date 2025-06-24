using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechnicalAssessment.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "Description", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("171c8825-c739-46a4-b66d-2bd7c2b5f056"), new Guid("809e7a3c-2895-41ea-9961-f375b12a1d41"), null, new DateTime(2025, 6, 24, 16, 17, 25, 446, DateTimeKind.Utc).AddTicks(3238), "Gaming Laptop", false, null, null, "Laptop", 1500m },
                    { new Guid("847274a4-8b0e-44a0-9c0e-516e73be4468"), new Guid("9e45b88a-6811-4d94-b00f-57e74d5a15ba"), null, new DateTime(2025, 6, 24, 16, 17, 25, 446, DateTimeKind.Utc).AddTicks(3261), "Latest Android", false, null, null, "Smartphone", 800m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
