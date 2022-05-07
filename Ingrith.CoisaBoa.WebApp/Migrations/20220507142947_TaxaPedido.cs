using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingrith.CoisaBoa.WebApp.Migrations
{
    public partial class TaxaPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxaEntrega",
                table: "Pedido",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxaEntrega",
                table: "Pedido");
        }
    }
}
