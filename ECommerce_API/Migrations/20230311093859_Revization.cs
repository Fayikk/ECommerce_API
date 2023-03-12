using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_API.Migrations
{
    public partial class Revization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Users",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "CreateDate");
        }
    }
}
