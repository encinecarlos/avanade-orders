using orders.email.Domain.Entities;
using orders.email.Domain.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace orders.email.Domain.Services.EmailHandler
{
    public class EmailServiceHandler : IMessageServicehandler
    {
        private ILogger<EmailServiceHandler> Logger { get; }
        private IConfiguration Configuration { get; }
        private ISendGridClient EmailClient { get; }

        public EmailServiceHandler(ILogger<EmailServiceHandler> logger, IConfiguration configuration, ISendGridClient emailClient)
        {
            Logger = logger;
            Configuration = configuration;
            EmailClient = emailClient;
        }

        public async Task SendMessage(Order order)
        {
            Logger.LogInformation("Send message to destination");
            var plainTextContent = $"Olá, {order.Customer.Name}\nA compra do item:\n {order.Products[0].ProductName}\n Valor: R$ {order.Products[0].Price}\n Foi efetuada com sucesso.";
            var htmlContent = $"Olá, {order.Customer.Name}\nA compra do item:\n <b>{order.Products[0].ProductName}</b>\n Valor: R$ <b>{order.Products[0].Price}</b>\n Foi efetuada com sucesso.";

            var message = new SendGridMessage
            {
                From = new EmailAddress(Configuration["Email:Email_origin"], "Carlos Encine"),
                Subject = $"Sobre a compra nº {order.OrderId}",
            };

            message.AddContent(MimeType.Text, plainTextContent);
            message.AddContent(MimeType.Html, htmlContent);
            message.AddTo(new EmailAddress(order.Customer.Email, order.Customer.Name));

            await EmailClient.SendEmailAsync(message);
        }
    }
}