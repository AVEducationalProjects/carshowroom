using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarShowRoom.Migrations
{
    public partial class CarModelsRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_Vendors_VendorId",
                table: "CarModels");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_Vendors_VendorId",
                table: "CarModels",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModels_Vendors_VendorId",
                table: "CarModels");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModels_Vendors_VendorId",
                table: "CarModels",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
