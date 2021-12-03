using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchoolSemi.Data.Migrations
{
    public partial class _201931081209 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Sastanak__Korisnik_OrganizatorId",
                table: "_Sastanak");

            migrationBuilder.DropIndex(
                name: "IX__Sastanak_OrganizatorId",
                table: "_Sastanak");

            migrationBuilder.DropColumn(
                name: "OrganizatorId",
                table: "_Sastanak");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizatorId",
                table: "_Sastanak",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__Sastanak_OrganizatorId",
                table: "_Sastanak",
                column: "OrganizatorId");

            migrationBuilder.AddForeignKey(
                name: "FK__Sastanak__Korisnik_OrganizatorId",
                table: "_Sastanak",
                column: "OrganizatorId",
                principalTable: "_Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
