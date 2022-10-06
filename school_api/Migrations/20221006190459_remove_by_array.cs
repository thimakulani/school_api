using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school_api.Migrations
{
    public partial class remove_by_array : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconFile",
                table: "School");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "IconFile",
                table: "School",
                type: "bytea",
                nullable: true);
        }
    }
}
