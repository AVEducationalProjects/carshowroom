using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarShowRoom.Migrations
{
    public partial class Partners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Partner_PartnerId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partner",
                table: "Partner");

            migrationBuilder.RenameTable(
                name: "Partner",
                newName: "Partners");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Partners",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partners",
                table: "Partners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Partners_PartnerId",
                table: "Cars",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Partners_PartnerId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partners",
                table: "Partners");

            migrationBuilder.RenameTable(
                name: "Partners",
                newName: "Partner");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Partner",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partner",
                table: "Partner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Partner_PartnerId",
                table: "Cars",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
