using LojaJkMisterG.Context;
using LojaJkMisterG.Models;

namespace LojaJkMisterG.Areas.Admin.Servicos
{
    public class GraficoVendasService
    {
        private readonly AppDbContext context;

        public GraficoVendasService(AppDbContext context)
        {
            this.context = context;
        }

        public List<RoupaGrafico> GetVendasRoupas(int dias = 360)
        {
            var data = DateTime.Now.AddDays(-dias);

            var roupas = (from pd in context.PedidosDetalhe
                          join r in context.Roupas on pd.RoupaId equals r.RoupaId
                          where pd.Pedido.PedidoEnviado >= data
                          group pd by new { pd.RoupaId, r.Nome }
                           into g
                          select new
                          {
                              RoupaNome = g.Key.Nome,
                              RoupasQuantidade = g.Sum(q => q.Quantidade),
                              RoupasValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                          });

            var lista = new List<RoupaGrafico>();

            foreach (var item in roupas)
            {
                var roupa = new RoupaGrafico();
                roupa.RoupaNome = item.RoupaNome;
                roupa.RoupasQuantidade = item.RoupasQuantidade;
                roupa.RoupasValorTotal = item.RoupasValorTotal;
                lista.Add(roupa);
            }

            return (lista);
        }
    }
}
