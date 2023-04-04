using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaJkMisterG.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao)" +
                
                "VALUES('Inverno', 'Roupas para se usar no frio')"); 
            
            
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao)" +
                
                "VALUES('Verão', 'Roupas para se usar no calor')"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
