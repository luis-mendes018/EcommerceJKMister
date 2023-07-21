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

       
    }


}
