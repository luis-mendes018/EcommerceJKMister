using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaJkMisterG.Migrations
{
    /// <inheritdoc />
    public partial class PedidoDetalhesUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pedidold",
                table: "Pedidos",
                newName: "PedidoId");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Pedidos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "Pedidos",
                newName: "Pedidold");
        }
    }
}
