using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pschool.Migrations
{
    public partial class AddFildsToParents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Parents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Parents");
        }
    }
}
