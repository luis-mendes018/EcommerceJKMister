using System.ComponentModel.DataAnnotations;
using LojaJkMisterG.Domain;

namespace LojaJkMisterG.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome de usuário")]
        [LoginValidation(ErrorMessage = "Formato de login inválido!")]
        [Display(Name = "Usuário")]
        [StringLength(50, ErrorMessage = "Limite de caracteres excedido!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }


    }
}
