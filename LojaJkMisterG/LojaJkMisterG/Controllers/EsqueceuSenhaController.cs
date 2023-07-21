using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaJkMisterG.Controllers
{
    public class EsqueceuSenhaController : Controller
    {
        [AllowAnonymous]
        public IActionResult EsqueceuSenhaView()
        {
            return View();
        }
    }
}
