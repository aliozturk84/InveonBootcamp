using InveonBootcamp.Business.Abstract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class EmailConsumer : IConsumer<EmailMessage>
    {
        private readonly IMailService _mailService;

        public EmailConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<EmailMessage> context)
        {
            var message = context.Message;
            await _mailService.SendMessageAsync(message.To, message.Subject, message.Body, message.IsBodyHtml);
        }
    }

}
