using LojaJkMisterG.Areas.Admin.Servicos.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace LojaJkMisterG.Areas.Admin.Servicos
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendConfirmationEmailAsync(string email, string confirmationLink)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Loja JK Mister G", _configuration["Email:From"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:SmtpPort"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }

        public async Task SendPasswordResetEmailAsync(string email, string callbackUrl)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Loja JK", "sua-conta@lojajk.com.br"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Redefinir senha da Loja JK";

            // Constrói o corpo do email
            var builder = new BodyBuilder();
            builder.HtmlBody = $"<p>Para redefinir sua senha, clique no link abaixo:</p><p><a href='{callbackUrl}'>Redefinir senha</a></p>";

            message.Body = builder.ToMessageBody();

            // Envia o email utilizando o serviço de email configurado
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("seu-email@gmail.com", "sua-senha");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}

