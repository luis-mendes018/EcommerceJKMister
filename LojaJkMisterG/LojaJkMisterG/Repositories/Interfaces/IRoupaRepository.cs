using LojaJkMisterG.Models;

namespace LojaJkMisterG.Repositories.Interfaces
{
    public interface IRoupaRepository
    {
        IEnumerable<Roupa> Roupas { get; }
        IEnumerable<Roupa> RoupasPreferidas { get; }
        Roupa GetRoupaById(int roupaId);
    }
}
