using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarShowRoom.Migrations
{
    public partial class CarDependencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarColors_ColorId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarModelId",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepotId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DepotId",
                table: "Cars",
                column: "DepotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarColors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "CarColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Depots_DepotId",
                table: "Cars",
                column: "DepotId",
                principalTable: "Depots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarColors_ColorId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Depots_DepotId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ClientId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DepotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DepotId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CarModelId",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarColors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "CarColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
