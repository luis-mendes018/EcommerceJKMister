using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaJkMisterG.Migrations
{
    /// <inheritdoc />
    public partial class PedidoDetalhesUp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Pedidos");
        }
    }
}
