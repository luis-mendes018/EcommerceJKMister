using System.Text;

using LojaJkMisterG.Areas.Admin.AdmViewModels;
using LojaJkMisterG.Context;
using LojaJkMisterG.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ReflectionIT.Mvc.Paging;

namespace LojaJkMisterG.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesTypes.Admin)]
    public class AdminFuncionariosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AdminFuncionariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: AdminFuncionariosController

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Email")
        {
            var usersQuery = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                usersQuery = usersQuery.Where(u => u.Email.Contains(filter));
            }
            //var pagedUsers = await usersQuery.ToPagedListAsync(pageindex, 5);

            var viewModelList = new List<AdminRegistroUsuarioEditViewModel>();
            foreach (var user in usersQuery)
            {
                var viewModel = new AdminRegistroUsuarioEditViewModel
                {
                    Id = user.Id,
                    EmailRegister = user.Email,
                    IsAdmin = await _userManager.IsInRoleAsync(user, RolesTypes.Admin),
                    IsGerente = await _userManager.IsInRoleAsync(user, RolesTypes.Gerente),
                    IsVendedor = await _userManager.IsInRoleAsync(user, RolesTypes.Vendedor)
                };
                viewModelList.Add(viewModel);
            }
            var lista = viewModelList.AsQueryable();
            var model = PagingList.Create(lista, 5, pageindex);

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "Index";
            return View(model);
        }





        // GET: AdminFuncionariosController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var viewModel = new AdminRegistroUsuarioEditViewModel
            {
                Id = user.Id,
                EmailRegister = user.Email,
                IsAdmin = userRoles.Contains(RolesTypes.Admin),
                IsGerente = userRoles.Contains(RolesTypes.Gerente),
                IsVendedor = userRoles.Contains(RolesTypes.Vendedor)
            };

            return View("Details", viewModel);
        }

        // GET: AdminFuncionariosController/Create
        public IActionResult Create()
        {
            return View(new AdminRegistroUsuarioEditViewModel());
        }

        // POST: AdminUsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminRegistroUsuarioEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                {
                    ModelState.AddModelError("", "Já existe um usuário com esse nome.");
                    return View(model);
                }

                userExists = await _userManager.FindByEmailAsync(model.EmailRegister);
                if (userExists != null)
                {
                    ModelState.AddModelError("", "Já existe um usuário com esse e-mail.");
                    return View(model);
                }


                var user = new IdentityUser { UserName = model.UserName, Email = model.EmailRegister };
                var result = await _userManager.CreateAsync(user, model.GeneratedPassword);


                if (result.Succeeded)
                {
                    if (model.IsVendedor)
                    {
                        await _userManager.AddToRoleAsync(user, RolesTypes.Vendedor);
                    }
                    if (model.IsGerente)
                    {
                        await _userManager.AddToRoleAsync(user, RolesTypes.Gerente);
                    }
                    if (model.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, RolesTypes.Admin);
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        // GET: AdminFuncionariosController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var model = new AdminFuncionarioEditViewModel
            {   UserName = user.UserName,
                EmailRegister = user.Email,
                IsVendedor = roles.Contains(RolesTypes.Vendedor),
                IsGerente = roles.Contains(RolesTypes.Gerente),
                IsAdmin = roles.Contains(RolesTypes.Admin)
            };

            return View(model);
        }

        // POST: AdminFuncionariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AdminFuncionarioEditViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }


                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null && userExists.Id != user.Id)
                {
                    ModelState.AddModelError("", "Já existe um usuário com esse nome.");
                    return View(model);
                }

                userExists = await _userManager.FindByEmailAsync(model.EmailRegister);
                if (userExists != null && userExists.Id != user.Id)
                {
                    ModelState.AddModelError("", "Já existe um usuário com esse e-mail.");
                    return View(model);
                }

                user.Email = model.EmailRegister;
                user.UserName = model.UserName;

                if (!string.IsNullOrEmpty(model.GeneratedPassword))
                {
                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    var passwordHash = passwordHasher.HashPassword(user, model.GeneratedPassword);
                    user.PasswordHash = passwordHash;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    await _userManager.RemoveFromRolesAsync(user, roles);

                    if (model.IsVendedor)
                    {
                        await _userManager.AddToRoleAsync(user, RolesTypes.Vendedor);
                    }
                    if (model.IsGerente)
                    {
                        await _userManager.AddToRoleAsync(user, RolesTypes.Gerente);
                    }
                    if (model.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, RolesTypes.Admin);
                    }
                    
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);

        }

        // GET: AdminFuncionariosController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new AdminRegistroUsuarioEditViewModel
            {
                Id = user.Id,
                EmailRegister = user.Email
            };

            return View(viewModel);
        }

        // POST: AdminFuncionariosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                var viewModel = new AdminRegistroUsuarioEditViewModel
                {
                    Id = user.Id,
                    EmailRegister = user.Email
                };

                return View(viewModel);
            }


        }

        public IActionResult GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=-{}[]|\\:;\"'<>,.?/";
            var password = new StringBuilder();
            var random = new Random();

            while (password.Length < 10)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return Ok(password.ToString());
        }
    }
}
