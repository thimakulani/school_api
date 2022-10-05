using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school_api.Migrations
{
    public partial class update_byte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "IconFile",
                table: "School",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconFile",
                table: "School");
        }
    }
}
