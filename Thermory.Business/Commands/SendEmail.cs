using System;
using System.Net.Mail;
using Thermory.Domain.Commands;

namespace Thermory.Business.Commands
{
    internal class SendEmail : ICommand
    {
        private readonly string _toAddress;
        private readonly string _fromAddress;
        private readonly string _subject;
        private readonly string _message;

        public SendEmail(string toAddress, string fromAddress, string subject, string message)
        {
            _toAddress = toAddress;
            _fromAddress = fromAddress;
            _subject = subject;
            _message = message;
        }

        public void Execute()
        {
            var message = new MailMessage(_fromAddress, _toAddress, _subject, _message) { IsBodyHtml = true };
            using (var client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}
