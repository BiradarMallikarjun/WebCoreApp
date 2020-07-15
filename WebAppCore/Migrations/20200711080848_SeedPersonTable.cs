using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppCore.Migrations
{
    public partial class SeedPersonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Age", "FirstName", "LastName" },
                values: new object[] { 1, 33, "Mallikarjun", "Biradar" });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Age", "FirstName", "LastName" },
                values: new object[] { 2, 31, "Manjunath", "Biradar" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
