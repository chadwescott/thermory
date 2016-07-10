using System.Net.Mail;
using Thermory.Domain.Commands;

namespace Thermory.Business.Commands
{
    internal class SendEmail : ICommand
    {
        public void Execute()
        {
            var message = new MailMessage("thermoryinventory@thermoryinventory.com", "chadwescott@gmail.com", "test",
                "This is a test email");
            using (var client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}
