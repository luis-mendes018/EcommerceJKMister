using LojaJkMisterG.Models;

namespace LojaJkMisterG.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
