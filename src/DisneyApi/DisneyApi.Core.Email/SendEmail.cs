namespace DisneyApi.Core.Email
{    
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System.Threading.Tasks;

    public class SendEmail
    {
        private string Secret { get; set; }
        private string EmailTo { get; set; }
        private readonly string Subject = "Acaba de registrarse";
        private readonly string Context = "Cuerpo del email";
        private readonly string HtmlContent = "<strong> Disney </strong>";

        public SendEmail(string secretKey, string emailTo)
        {
            Secret = secretKey;
            EmailTo = emailTo;
        }

        public async Task  Send()
        {
            var client = new SendGridClient(Secret);
            var EmailFrom = new EmailAddress("prgazure@gmail.com", "Email de prueba");
            var EmailDestido = new EmailAddress(EmailTo, "Destino");

            var mensaje = MailHelper.CreateSingleEmail(EmailFrom, EmailDestido, Subject, Context, HtmlContent);

            await client.SendEmailAsync(mensaje);

        }
    }
}
