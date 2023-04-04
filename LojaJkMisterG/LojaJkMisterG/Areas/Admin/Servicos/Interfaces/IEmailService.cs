namespace LojaJkMisterG.Areas.Admin.Servicos.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationLink);
        Task SendPasswordResetEmailAsync(string email, string callbackUrl);
    }
}
