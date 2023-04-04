using LojaJkMisterG.Repositories.Interfaces;
using LojaJkMisterG.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaJkMisterG.Controllers
{
    
    [Authorize]
    public class RoupaPreferidaController : Controller
    {
        private readonly IRoupaRepository _roupaRepository;

        public RoupaPreferidaController(IRoupaRepository roupaRepository)
        {
            _roupaRepository = roupaRepository;
        }

        [Authorize]
        public IActionResult RoupaPreferida()
        {
            var roupaPreferidaViewModel = new RoupaPreferidaViewModel 
            {
                RoupasPreferidas = _roupaRepository.RoupasPreferidas
            };
            return View(roupaPreferidaViewModel);
        }
    }
}
