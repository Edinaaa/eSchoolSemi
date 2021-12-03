using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class Tokenzadvofaktorsku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "korisnickiNalogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenVaziDo",
                table: "korisnickiNalogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "korisnickiNalogs");

            migrationBuilder.DropColumn(
                name: "TokenVaziDo",
                table: "korisnickiNalogs");
        }
    }
}
