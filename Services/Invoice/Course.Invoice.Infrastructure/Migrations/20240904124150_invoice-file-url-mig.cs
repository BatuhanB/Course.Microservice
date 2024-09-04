using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class invoicefileurlmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                schema: "invoices",
                table: "OrderInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InvoiceFileUrl",
                schema: "invoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    InvoiceId = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    BuyerId = table.Column<string>(type: "text", nullable: false),
                    InvoiceCreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFileUrl", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceFileUrl",
                schema: "invoices");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "invoices",
                table: "OrderInformation");
        }
    }
}
