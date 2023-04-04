using LojaJkMisterG.Context;
using LojaJkMisterG.Models;
using LojaJkMisterG.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaJkMisterG.Repositories
{
    public class RoupaRepository : IRoupaRepository
    {
        private readonly AppDbContext _context;
        public RoupaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<Roupa> Roupas => _context.Roupas.Include(c => c.Categoria);

        public IEnumerable<Roupa> RoupasPreferidas => _context.Roupas.
                            Where(r=> r.IsRoupaPreferida)
                           .Include(c=> c.Categoria);

        public Roupa GetRoupaById(int roupaId)
        {
            return _context.Roupas.FirstOrDefault(r=> r.RoupaId == roupaId);
        }
    }
}
