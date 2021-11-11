using MassTransit;
using MediatR;
using SimpleMailService___SMS.Domain.Commands;
using SimpleMailService___SMS.Domain.Contracts;
using SimpleMailService___SMS.Domain.Notification;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Domain.Consumers
{
	public class QueueSendEmailConsumer : IConsumer<QueueSendEmailComand>
	{
		private readonly IMediator _bus;
		private readonly IDomainNotificationContext _notificationContext;
		private readonly ISendEmailService _sendEmailService;

		public QueueSendEmailConsumer(IMediator bus, IDomainNotificationContext notificationContext, ISendEmailService sendEmailService)
		{
			_bus = bus;
			_notificationContext = notificationContext;
			_sendEmailService = sendEmailService;
		}

		public async Task Consume(ConsumeContext<QueueSendEmailComand> context)
		{

			if (context.Message is SendEmailComand email)
				await _sendEmailService.Send(email);
		}
	}
}
