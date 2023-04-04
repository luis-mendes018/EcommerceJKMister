using LojaJkMisterG.Models;

namespace LojaJkMisterG.ViewModels
{
    public class PedidoRoupaViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
