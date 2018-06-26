using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ELTE.TravelAgency.Migrations
{
    public partial class ApUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Apartments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_CityId",
                table: "Apartments",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Cities_CityId",
                table: "Apartments",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Cities_CityId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_CityId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Apartments");
        }
    }
}
