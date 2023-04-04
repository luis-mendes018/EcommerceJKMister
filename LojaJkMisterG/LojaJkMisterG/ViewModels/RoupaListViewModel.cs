using System.ComponentModel.DataAnnotations;

using LojaJkMisterG.Models;

namespace LojaJkMisterG.ViewModels
{
    public class RoupaListViewModel
    {
        public IEnumerable<Roupa> Roupas { get; set; }

        public string CategoriaAtual { get; set; }
    }
}
