using LojaJkMisterG.Models;
using LojaJkMisterG.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LojaJkMisterG.Controllers
{
    public class AccountAdmController : Controller
    {

        private readonly UserManager<IdentityUser> _admManager;
        private readonly SignInManager<IdentityUser> _signInadmManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountAdmController(UserManager<IdentityUser> admManager, SignInManager<IdentityUser> signInadmManager, RoleManager<IdentityRole> roleManager)
        {
            _admManager = admManager;
            _signInadmManager = signInadmManager;
            _roleManager = roleManager;
        }

        //Área de admistração
        [AllowAnonymous]
        public IActionResult LoginAdm(string returnUrl)
        {
            return View(new LoginAdmViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAdm(LoginAdmViewModel loginAdm)
        {
            bool RoleValidator;
            if (!ModelState.IsValid)
                return View(loginAdm);
            var user = await _admManager.FindByNameAsync(loginAdm.AdmName);
            if (user != null)
            {
                RoleValidator = await _admManager.IsInRoleAsync(user, RolesTypes.Admin);
                if (RoleValidator == true)
                {
                    var result = await _signInadmManager.PasswordSignInAsync(user, loginAdm.PasswordAdm, false, false);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(loginAdm.ReturnUrl))
                        {

                            return RedirectToAction(actionName: "Index", "Admin");
                        }
                        return Redirect(loginAdm.ReturnUrl);
                    }
                }
                else
                {
                    RoleValidator = await _admManager.IsInRoleAsync(user, RolesTypes.Gerente);
                    if (RoleValidator == true)
                    {
                        if (user != null)
                        {
                            var result = await _signInadmManager.PasswordSignInAsync(user, loginAdm.PasswordAdm, false, false);
                            if (result.Succeeded)
                            {
                                if (string.IsNullOrEmpty(loginAdm.ReturnUrl))
                                {
                                    return RedirectToAction(actionName: "Index", controllerName: "Admin");
                                }
                                return Redirect(loginAdm.ReturnUrl);
                            }
                        }
                    }
                    else
                    {
                        RoleValidator = await _admManager.IsInRoleAsync(user, RolesTypes.Vendedor);
                        if (RoleValidator == true)
                        {
                            if (user != null)
                            {
                                var result = await _signInadmManager.PasswordSignInAsync(user, loginAdm.PasswordAdm, false, false);
                                if (result.Succeeded)
                                {
                                    if (string.IsNullOrEmpty(loginAdm.ReturnUrl))
                                    {
                                        return RedirectToAction(actionName: "Index", controllerName: "Admin");
                                    }
                                    return Redirect(loginAdm.ReturnUrl);
                                }
                            }
                        }
                        
                    }
                }
            }
            //else
            //{
            //    //var funcionario = await _admManager.FindByNameAsync(loginAdm.AdmName);

            //    //if (funcionario != null)
            //    //{
            //    //    var result = await _signInadmManager.PasswordSignInAsync(funcionario, loginAdm.PasswordAdm, false, false);
            //    //    if (result.Succeeded)
            //    //    {
            //    //        if (string.IsNullOrEmpty(loginAdm.ReturnUrl))
            //    //        {
            //    //            return RedirectToAction( actionName:"Index", controllerName: "Admin");
            //    //        }
            //    //        return Redirect(loginAdm.ReturnUrl);
            //    //    }
            //    //}
            //}

            ModelState.AddModelError(string.Empty, "Credenciais incorretas!");
            return View("LoginAdm", loginAdm);

        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
