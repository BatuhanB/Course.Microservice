using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderitemsproductownerid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductOwnerId",
                schema: "ordering",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOwnerId",
                schema: "ordering",
                table: "OrderItems");
        }
    }
}
