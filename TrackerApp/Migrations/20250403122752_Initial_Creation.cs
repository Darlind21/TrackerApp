using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailySummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDate = table.Column<DateOnly>(type: "date", nullable: false),
                    WorkingDuration = table.Column<int>(type: "int", nullable: false),
                    BreakDuration = table.Column<int>(type: "int", nullable: false),
                    AwayDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailySummaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusActivities_DailySummaries_DailySummaryId",
                        column: x => x.DailySummaryId,
                        principalTable: "DailySummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusActivities_DailySummaryId",
                table: "StatusActivities",
                column: "DailySummaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusActivities");

            migrationBuilder.DropTable(
                name: "DailySummaries");
        }
    }
}
