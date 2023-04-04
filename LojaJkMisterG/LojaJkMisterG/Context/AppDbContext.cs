using LojaJkMisterG.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LojaJkMisterG.ViewModels;
using LojaJkMisterG.Areas.Admin.AdmViewModels;

namespace LojaJkMisterG.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Roupa> Roupas { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidosDetalhe { get; set; }
        public DbSet<LojaJkMisterG.Areas.Admin.AdmViewModels.AdminRegistroFuncionarioViewModel> AdminRegistroFuncionarioViewModel { get; set; }




    }
}