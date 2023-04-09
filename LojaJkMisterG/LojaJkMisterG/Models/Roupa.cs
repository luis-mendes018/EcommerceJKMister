using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaJkMisterG.Models
{
    [Table("Roupas")]
    public class Roupa
    {
        [Key]
        public int RoupaId { get; set; }

        [Required(ErrorMessage = "Informe o nome da roupa!")]
        [Display(Name = "Nome da Roupa")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 80 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da roupa deve ser informada!")]
        [Display(Name = "Descrição breve da Roupa")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder (1) caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição da roupa deve ser informada!")]
        [Display(Name = "Descrição detalhada da Roupa")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição pode exceder (1) caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o preço da roupa!")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1,00 e 999,00")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }


        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Favoritar roupa")]
        public bool IsRoupaPreferida { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public  int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
