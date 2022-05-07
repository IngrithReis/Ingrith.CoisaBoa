using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingrith.CoisaBoa.WebApp.Migrations
{
    public partial class PagamentoEObservacaoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Pedido",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pagamento",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "Pagamento",
                table: "Pedido");
        }
    }
}
