using System.Net.Mail;
using System.Net;
using LojaJkMisterG.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace LojaJkMisterG.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
       
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnurl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnurl
            });
        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("List", "Roupa");
                    }

                    return Redirect(loginVM.ReturnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Credenciais incorretas!");
            return View("Login", loginVM);

        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CadastroViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registroVM.UserName,
                    Email = registroVM.EmailRegister,
                };

                var userWithSameUsername = await _userManager.FindByNameAsync(registroVM.UserName);
                if (userWithSameUsername != null)
                {
                    this.ModelState.AddModelError("UserName", "Já existe um usuário com esse nome");
                    return View(registroVM);
                }

                // Verifica se o endereço de e-mail já existe
                var userWithSameEmail = await _userManager.FindByEmailAsync(registroVM.EmailRegister);
                if (userWithSameEmail != null)
                {
                    this.ModelState.AddModelError("EmailRegister", "Já existe um usuário com esse endereço de e-mail");
                    return View(registroVM);
                }


                var result = await _userManager.CreateAsync(user, registroVM.Password);


                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "Member");

                    //// Cria o token de confirmação de e-mail
                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    //// Cria a URL de confirmação de e-mail
                    //var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    //// Cria o corpo do e-mail
                    //var message = new MailMessage();
                    //message.To.Add(user.Email);
                    //message.Subject = "Confirmação de E-mail";
                    //message.Body = $"Por favor, confirme sua conta clicando neste link: {confirmationLink}";
                    //message.From = new MailAddress("seu-email@gmail.com");

                    //// Envia o e-mail usando SMTP
                    //var smtpClient = new SmtpClient("smtp.gmail.com")
                    //{
                    //    Port = 587,
                    //    Credentials = new NetworkCredential("seu-email@gmail.com", "sua-senha"),
                    //    EnableSsl = true,
                    //};
                    //smtpClient.Send(message);

                    TempData["Mensagem"] = "\nCadastro realizado com sucesso! Efetue o login.\n";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }

            return View(registroVM);
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (userId == null || token == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    if (result.Succeeded)
        //    {
        //        return View("ConfirmEmail");
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
