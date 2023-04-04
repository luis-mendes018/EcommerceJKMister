using LojaJkMisterG.Domain;
using System.ComponentModel.DataAnnotations;

namespace LojaJkMisterG.Areas.Admin.AdmViewModels
{
    public class AdminAlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe sua senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string AdminPasswordNow { get; set; }

        [Required(ErrorMessage = "Informe sua nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string AdminPasswordNew { get; set; }

        [Required(ErrorMessage = "Confirme sua nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare(nameof(AdminPasswordNew), ErrorMessage = "A senha e a confirmação" + " não estão iguais")]
        [SenhaValidation(ErrorMessage = "Formato de senha inválido")]
        public string AdminPasswordNewConfirm { get; set; }
    }
}
