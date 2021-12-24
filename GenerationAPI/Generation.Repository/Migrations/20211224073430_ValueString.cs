using Microsoft.EntityFrameworkCore.Migrations;

namespace Generation.Repository.Migrations
{
    public partial class ValueString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TimedValues",
                type: "nvarchar(MAX)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "TimedValues",
                type: "decimal(5, 2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true);
        }
    }
}
