using LojaJkMisterG.Models;
using LojaJkMisterG.Repositories.Interfaces;
using LojaJkMisterG.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LojaJkMisterG.Controllers
{
    
    [Authorize]
    public class RoupaController : Controller
    {
        private readonly IRoupaRepository _roupaRepository;

        public RoupaController(IRoupaRepository roupaRepository)
        {
            _roupaRepository = roupaRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Roupa> roupas;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                roupas = _roupaRepository.Roupas.OrderBy(r => r.RoupaId);

                categoriaAtual = "Todas as roupas";
            }
            else
            {
                #region código para categorias fixas
                //if(string.Equals("Inverno", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    roupas = _roupaRepository.Roupas
                //        .Where(r => r.Categoria.CategoriaNome.Equals("Inverno"))
                //        .OrderBy(r => r.Nome);
                //}
                //else
                //{
                //    roupas = _roupaRepository.Roupas
                //        .Where(r => r.Categoria.CategoriaNome.Equals("Verão"))
                //        .OrderBy(r => r.Nome);
                //}
                #endregion

                roupas = _roupaRepository.Roupas
                    .Where(r => r.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(r => r.Nome);

                categoriaAtual = categoria;
            }

            ////
            var roupasListViewModel = new RoupaListViewModel
            {
                Roupas = roupas,
                CategoriaAtual = categoriaAtual,
            };

            return View(roupasListViewModel);
        }

        public IActionResult Details(int roupaId)
        {
            var roupa = _roupaRepository.Roupas.FirstOrDefault(r => r.RoupaId == roupaId);

            return View(roupa);
        }

      
        public ViewResult Search(string searchString)
        {
            IEnumerable<Roupa> roupas;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                roupas = _roupaRepository.Roupas.OrderBy(p => p.RoupaId);
                categoriaAtual = "Todos os Roupas";

            }
            else
            {
                roupas = _roupaRepository.Roupas
                    .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (roupas.Any())
                    categoriaAtual = "Roupas";
                else
                    categoriaAtual = "Nenhuma roupa foi encontrada";
            }

            return View("~/Views/Roupa/List.cshtml", new RoupaListViewModel
            {
                Roupas = roupas,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}
