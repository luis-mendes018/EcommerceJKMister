using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaJkMisterG.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoFuncionarioViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Password",
            table: "AdminRegistroFuncionarioViewModel");

            migrationBuilder.DropColumn(
                name: "PasswordConfirm",
                table: "AdminRegistroFuncionarioViewModel");

            migrationBuilder.AddColumn<string>(
                name: "GeneratedPassword",
                table: "AdminRegistroFuncionarioViewModel",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "Password",
           table: "AdminRegistroFuncionarioViewModel",
            nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordConfirm",
                table: "AdminRegistroFuncionarioViewModel",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "GeneratedPassword",
                table: "AdminRegistroFuncionarioViewModel");
        }
    }
}
