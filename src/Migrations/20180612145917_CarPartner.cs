using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarShowRoom.Migrations
{
    public partial class CarPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Requisites = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PartnerId",
                table: "Cars",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Partner_PartnerId",
                table: "Cars",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Partner_PartnerId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PartnerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Cars");
        }
    }
}
