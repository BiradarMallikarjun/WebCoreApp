using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppCore.Migrations
{
    public partial class ChangedColumnNamePhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Person");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
