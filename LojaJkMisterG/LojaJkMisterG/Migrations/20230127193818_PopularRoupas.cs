using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaJkMisterG.Migrations
{
    /// <inheritdoc />
    public partial class PopularRoupas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId, Nome, DescricaoCurta, DescricaoDetalhada, Preco, ImagemUrl, ImagemThumbnailUrl, IsRoupaPreferida, EmEstoque) " + "VALUES (1, 'Camiseta de manga comprida', 'Ótima para frios leves', 'Roupa usada para frios leves e ajuda a manter o estilo', 50.00, 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 0, 1)");
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId, Nome, DescricaoCurta, DescricaoDetalhada, Preco, ImagemUrl, ImagemThumbnailUrl, IsRoupaPreferida, EmEstoque) " + "VALUES (1, 'Camiseta de manga comprida', 'Ótima para frios leves', 'Roupa usada para frios leves e ajuda a manter o estilo', 50.00, 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 0, 1)");
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId, Nome, DescricaoCurta, DescricaoDetalhada, Preco, ImagemUrl, ImagemThumbnailUrl, IsRoupaPreferida, EmEstoque) " + "VALUES (1, 'Camiseta de manga comprida', 'Ótima para frios leves', 'Roupa usada para frios leves e ajuda a manter o estilo', 50.00, 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 0, 1)");
            migrationBuilder.Sql("INSERT INTO Roupas(CategoriaId, Nome, DescricaoCurta, DescricaoDetalhada, Preco, ImagemUrl, ImagemThumbnailUrl, IsRoupaPreferida, EmEstoque) " + "VALUES (2, 'Camiseta de manga curta', 'Ótima para calor', 'Roupa usada para calor e ajuda a manter o estilo', 50.00, 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 'https://photos.enjoei.com.br/blusa-de-frio-la-manga-comprida-80085517/828xN/czM6Ly9waG90b3MuZW5qb2VpLmNvbS5ici9wcm9kdWN0cy8xMDg4Nzc0Ny9lZDc0ZTQ5MDNlYWYyYzhiMTE5MjVjOTMxN2QzNTA0NS5qcGc', 1, 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Roupas");
        }
    }
}
