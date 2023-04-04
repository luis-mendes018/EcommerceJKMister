using LojaJkMisterG.Areas.Admin.AdmViewModels;
using LojaJkMisterG.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LojaJkMisterG.Areas.Admin.Controllers
{


    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _admManager;

        public AdminController(UserManager<IdentityUser> admManager)
        {
            _admManager = admManager;
        }

        [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.Vendedor + "," + RolesTypes.Gerente)]
        public IActionResult Index()
        {
            return View();
        }

        //Área de administração
        [Authorize]
        public IActionResult AdminAlterarSenhaView()
        {
            return View(new AdminAlterarSenhaViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AdminAlterarSenhaView(AdminAlterarSenhaViewModel admAlterarSenha)
        {
            if (!ModelState.IsValid)
            {
                return View(admAlterarSenha);
            }

            var user = await _admManager.GetUserAsync(User);

            var result = await _admManager.CheckPasswordAsync(user, admAlterarSenha.AdminPasswordNow);

            if (!result)
            {
                ModelState.AddModelError("PasswordNow", "A senha atual fornecida está incorreta.");
                return View(admAlterarSenha);
            }

            var newPassword = _admManager.PasswordHasher.HashPassword(user, admAlterarSenha.AdminPasswordNew);
            user.PasswordHash = newPassword;
            var updateResult = await _admManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar a senha do usuário.");
                return View(admAlterarSenha);
            }
            else
            {
                TempData["SuccessMessage"] = "\nSenha alterada com suscesso!\n";

            }

            return View(admAlterarSenha);

        }
    }


}
