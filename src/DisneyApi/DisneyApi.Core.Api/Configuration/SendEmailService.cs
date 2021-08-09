namespace DisneyApi.Core.Api.Configuration
{
    using DisneyApi.Core.Models.Entities;
    using Microsoft.Extensions.Logging;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System;
    using System.Threading.Tasks;

    public class SendEmailService : IMailService
    {
        public readonly ISendGridClient _sendGridClient;
        private readonly ILogger<SendEmailService> _logger;
        private readonly string Subject = "Acaba de registrarse";
        private readonly string Context = "Cuerpo del email";
        private readonly string HtmlContent = "<strong> Disney </strong>";

        public SendEmailService(ISendGridClient sendGridClient, ILogger<SendEmailService> logger)
        {
            _sendGridClient = sendGridClient;
            _logger = logger;
        }
        public async Task SendEmialAsync(User user)
        {
            try
            {
                _logger.LogInformation($"Enviando Email a {user.Email}");

                var EmailFrom = new EmailAddress("prg@live.com.ar", "Email de prueba");
                var EmailDestido = new EmailAddress(user.Email, "Destino");

                var mensaje = MailHelper.CreateSingleEmail(EmailFrom, EmailDestido, Subject, Context, HtmlContent);

                var response = await _sendGridClient.SendEmailAsync(mensaje);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Correcto; {response.Body.ReadAsStringAsync()}");
                }
                else
                {
                    _logger.LogInformation($"Error al enviar email; {response.Body.ReadAsStringAsync()}");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exepcion al enviar email; {ex}");
                
            }
        }
    }
}
