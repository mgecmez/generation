using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Generation.Repository.Migrations
{
    public partial class ValueDecimalPrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerPlants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WebId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPlants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Good = table.Column<bool>(nullable: true),
                    Value = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    PowerPlantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimedValues_PowerPlants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimedValues_PowerPlantId",
                table: "TimedValues",
                column: "PowerPlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimedValues");

            migrationBuilder.DropTable(
                name: "PowerPlants");
        }
    }
}
