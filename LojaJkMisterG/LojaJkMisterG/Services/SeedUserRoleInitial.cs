using System.Security.Claims;
using LojaJkMisterG.Context;
using LojaJkMisterG.Models;

using Microsoft.AspNetCore.Identity;

namespace LojaJkMisterG.Servicos
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedRolesAync()
        {
            IdentityResult roleResult;
            var roleExist = await _roleManager.RoleExistsAsync(RolesTypes.Cliente);
            if (!roleExist)
            {
                var role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                roleResult = await _roleManager.CreateAsync(role);
            }

            roleExist = await _roleManager.RoleExistsAsync(RolesTypes.Admin);
            if (!roleExist)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                roleResult = await _roleManager.CreateAsync(role);
            }


            if (!await _roleManager.RoleExistsAsync(RolesTypes.Vendedor))
            {
                IdentityRole role = new IdentityRole();
                role.Name = RolesTypes.Vendedor;
                role.NormalizedName = "VENDEDOR";
                roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync(RolesTypes.Gerente))
            {
                IdentityRole role = new IdentityRole();
                role.Name = RolesTypes.Gerente;
                role.NormalizedName = "GERENTE";
                roleResult = await _roleManager.CreateAsync(role);
            }

        }



        public async Task SeedUsersAync()
        {
            if (await _userManager.FindByEmailAsync("usuario@localhost") == null)
            {
                var roleUser = await _roleManager.FindByNameAsync(RolesTypes.Cliente);
                var user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#20");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesTypes.Cliente);
                }
            }



            if (await _userManager.FindByEmailAsync("vendedor@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "vendedor@localhost";
                user.Email = "vendedor@localhost";
                user.NormalizedUserName = "VENDEDOR@LOCALHOST";
                user.NormalizedEmail = "VENDEDOR@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#20");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesTypes.Vendedor);
                }
            }

            if ( await _userManager.FindByEmailAsync("gerente@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "gerente@localhost";
                user.Email = "gerente@localhost";
                user.NormalizedUserName = "GERENTE@LOCALHOST";
                user.NormalizedEmail = "GERENTE@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#20");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesTypes.Gerente);
                }
            }




            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Numsey#20");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesTypes.Admin);
                }
            }
        }
    }
}