using MediatR;
using SimpleMailService___SMS.Domain.Commands;
using SimpleMailService___SMS.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Domain.CommandHandlers
{
	public class SendEmailComandHandler : AsyncRequestHandler<SendEmailComand>
	{
		private readonly ISendEmailService _sendEmailService;

		public SendEmailComandHandler(ISendEmailService sendEmailService)
		{
			_sendEmailService = sendEmailService;
		}

		protected override Task Handle(SendEmailComand request, CancellationToken cancellationToken)
		{
			return _sendEmailService.Send(request);
		}
	}
}
