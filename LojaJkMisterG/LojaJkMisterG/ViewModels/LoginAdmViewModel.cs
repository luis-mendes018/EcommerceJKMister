using LojaJkMisterG.Domain;
using System.ComponentModel.DataAnnotations;

namespace LojaJkMisterG.ViewModels
{
    public class LoginAdmViewModel
    {
        [Required(ErrorMessage = "Informe o login do admnistrador")]
        [Display(Name = "Administrador")]
        [EmailAddress(ErrorMessage = "Email do administrador inválido!")]
        [StringLength(50, ErrorMessage = "Limite de caracteres excedido!")]
        public string AdmName { get; set; }

        [Required(ErrorMessage = "Informe a senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string PasswordAdm { get; set; }
        public string ReturnUrl { get; set; }
    }
}
