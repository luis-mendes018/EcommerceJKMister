﻿using LojaJkMisterG.Areas.Admin.AdmViewModels;
using LojaJkMisterG.Context;
using LojaJkMisterG.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

using X.PagedList;

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

            var viewModelList = new List<AdminRegistroFuncionarioViewModel>();
            foreach (var user in usersQuery)
            {
                var viewModel = new AdminRegistroFuncionarioViewModel
                {
                    Id = user.Id,
                    EmailRegister = user.Email,
                    Password = user.PasswordHash,
                    PasswordConfirm = user.PasswordHash,
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
            var viewModel = new AdminRegistroFuncionarioViewModel
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
            return View(new AdminRegistroFuncionarioViewModel());
        }

        // POST: AdminFuncionariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminRegistroFuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.EmailRegister, Email = model.EmailRegister };
                var result = await _userManager.CreateAsync(user, model.Password);

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

            var model = new AdminRegistroFuncionarioViewModel
            {
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
        public async Task<IActionResult> Edit(string id, AdminRegistroFuncionarioViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Email = model.EmailRegister;
                user.UserName = model.EmailRegister;

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

            var viewModel = new AdminRegistroFuncionarioViewModel
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

                var viewModel = new AdminRegistroFuncionarioViewModel
                {
                    Id = user.Id,
                    EmailRegister = user.Email
                };

                return View(viewModel);
            }
        }
    }
}
