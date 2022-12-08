using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school_api.Migrations
{
    public partial class update_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saburb",
                table: "Address",
                newName: "Suburb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "Address",
                newName: "Saburb");
        }
    }
}
