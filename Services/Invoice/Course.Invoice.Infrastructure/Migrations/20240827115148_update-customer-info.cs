using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Invoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatecustomerinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_FirstName",
                schema: "invoices",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "Customer_LastName",
                schema: "invoices",
                table: "Invoice",
                newName: "Customer_UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_UserName",
                schema: "invoices",
                table: "Invoice",
                newName: "Customer_LastName");

            migrationBuilder.AddColumn<string>(
                name: "Customer_FirstName",
                schema: "invoices",
                table: "Invoice",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
