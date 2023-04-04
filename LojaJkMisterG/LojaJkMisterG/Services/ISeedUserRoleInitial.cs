using Microsoft.AspNetCore.Identity;

namespace LojaJkMisterG.Servicos
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRolesAync();
        Task SeedUsersAync();

    }
}
