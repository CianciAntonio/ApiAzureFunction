using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkClassLibrary.Migrations
{
    public partial class CustomerSurName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customer",
                newName: "SurName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "Customer",
                newName: "Name");
        }
    }
}
