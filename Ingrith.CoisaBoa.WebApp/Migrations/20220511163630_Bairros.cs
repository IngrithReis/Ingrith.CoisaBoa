using Microsoft.EntityFrameworkCore.Migrations;

namespace Ingrith.CoisaBoa.WebApp.Migrations
{
    public partial class Bairros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BairrosAtendidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BairrosAtendidos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BairrosAtendidos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Vila Planalto" },
                    { 2, "Tangamandápio" },
                    { 3, "Gopoúva" },
                    { 4, "Vila Sésamo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BairrosAtendidos");
        }
    }
}
