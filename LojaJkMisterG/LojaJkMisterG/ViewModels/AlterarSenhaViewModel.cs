using System.ComponentModel.DataAnnotations;
using LojaJkMisterG.Domain;

namespace LojaJkMisterG.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe sua senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string PasswordNow { get; set; }

        [Required(ErrorMessage = "Informe sua nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string PasswordNew { get; set; }

        [Required(ErrorMessage = "Confirme sua nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare(nameof(PasswordNew), ErrorMessage = "A senha e a confirmação" + " não estão iguais")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string PasswordNewConfirm { get; set; }
    }
}
