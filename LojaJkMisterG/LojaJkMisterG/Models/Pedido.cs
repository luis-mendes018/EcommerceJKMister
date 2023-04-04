using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LojaJkMisterG.Domain;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LojaJkMisterG.Models
{
    public class Pedido
    {
        
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50, ErrorMessage = "O nome ultrapassou o limite de 50 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o seu sobrenome")]
        [StringLength(50, ErrorMessage = "O sobrenome ultrapassou o limite de 50 caracteres!")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o seu endereço!")]
        [StringLength(100, ErrorMessage = "O endereço ultrapassou o limite de 100 caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Complemento")]
        [ComplementoValidation(ErrorMessage = "Complemento inválido")]
        public string Endereco2 { get; set; }

        [Required(ErrorMessage = "Informe o número da residência!")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Informe o seu bairro!")]
        [StringLength(50, ErrorMessage = "Limite máximo é de 50 caracteres!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe o seu CEP!")]
        [Display(Name = "CEP")]
        [CepValidation(ErrorMessage = "CEP inválido!")]
        [MaxLength(9)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe o seu CPF!")]
        [CpfValidation]
        [MaxLength(14)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Insira o nome do estado!")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Insira o nome da cidade!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o email.")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "O email não possui um formato correto")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do Pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Itens no Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }


        [Display(Name = "Entregue em")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregueEm { get; set; }
        
        public List<PedidoDetalhe> Pedidoltens { get; set; }



}   }
