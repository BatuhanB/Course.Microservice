﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "invoices");

            migrationBuilder.CreateTable(
                name: "OrderInformation",
                schema: "invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BuyerId = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Customer_FirstName = table.Column<string>(type: "text", nullable: false),
                    Customer_LastName = table.Column<string>(type: "text", nullable: false),
                    Customer_Address_Province = table.Column<string>(type: "text", nullable: false),
                    Customer_Address_District = table.Column<string>(type: "text", nullable: false),
                    Customer_Address_Street = table.Column<string>(type: "text", nullable: false),
                    Customer_Address_ZipCode = table.Column<string>(type: "text", nullable: false),
                    Customer_Address_Line = table.Column<string>(type: "text", nullable: false),
                    OrderInformationId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_OrderInformation_OrderInformationId",
                        column: x => x.OrderInformationId,
                        principalSchema: "invoices",
                        principalTable: "OrderInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    ProductOwnerId = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OrderInformationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_OrderInformation_OrderInformationId",
                        column: x => x.OrderInformationId,
                        principalSchema: "invoices",
                        principalTable: "OrderInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OrderInformationId",
                schema: "invoices",
                table: "Invoice",
                column: "OrderInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderInformationId",
                schema: "invoices",
                table: "OrderItem",
                column: "OrderInformationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "invoices");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "invoices");

            migrationBuilder.DropTable(
                name: "OrderInformation",
                schema: "invoices");
        }
    }
}
