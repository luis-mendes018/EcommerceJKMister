using LojaJkMisterG.Context;
using LojaJkMisterG.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaJkMisterG.Areas.Admin.Servicos
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;
        public RelatorioVendasService(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            return await resultado
                         .Include(l => l.Pedidoltens)
                         .ThenInclude(r => r.Roupa)
                         .OrderByDescending(x => x.PedidoEnviado)
                         .ToListAsync();
        }
    }
}

