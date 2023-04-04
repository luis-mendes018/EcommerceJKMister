using LojaJkMisterG.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LojaJkMisterG.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AlterarSenhaController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult AlterarSenhaView()
        {
            return View(new AlterarSenhaViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AlterarSenhaView(AlterarSenhaViewModel alterarSenha)
        {
            if (!ModelState.IsValid)
            {
                return View(alterarSenha);
            }

            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.CheckPasswordAsync(user, alterarSenha.PasswordNow);

            if (!result)
            {
                ModelState.AddModelError("PasswordNow", "A senha atual fornecida está incorreta.");
                return View(alterarSenha);
            }

            var newPassword = _userManager.PasswordHasher.HashPassword(user, alterarSenha.PasswordNew);
            user.PasswordHash = newPassword;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar a senha do usuário.");
                return View(alterarSenha);
            }
            else
            {
                TempData["SuccessMessage"] = "\nSenha alterada com suscesso!\n";
                
            }

            return View(alterarSenha);

        }
    }
}
