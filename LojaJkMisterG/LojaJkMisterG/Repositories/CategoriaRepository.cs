using LojaJkMisterG.Context;
using LojaJkMisterG.Models;
using LojaJkMisterG.Repositories.Interfaces;

namespace LojaJkMisterG.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
