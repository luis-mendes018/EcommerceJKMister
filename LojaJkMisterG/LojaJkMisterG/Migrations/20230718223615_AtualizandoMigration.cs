using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaJkMisterG.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "AdminFuncionarioEditViewModel",
          columns: table => new
          {
              Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
              EmailRegister = table.Column<string>(type: "nvarchar(max)", nullable: false),
              IsVendedor = table.Column<bool>(type: "bit", nullable: false),
              IsGerente = table.Column<bool>(type: "bit", nullable: false),
              IsAdmin = table.Column<bool>(type: "bit", nullable: false),
              GeneratedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
           constraints: table =>
           {
               table.PrimaryKey("PK_AdminFuncionarioEditViewModel", x => x.Id);
           });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRegistroFuncionarioViewModel");

            migrationBuilder.DropTable(
            name: "AdminFuncionarioEditViewModel");
        }
    }
}
