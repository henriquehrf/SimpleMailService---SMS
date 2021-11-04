using MediatR;
using SimpleMailService___SMS.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Domain.CommandHandlers
{
	public class SendEmailComandHandler : AsyncRequestHandler<SendEmailComand>
	{
		public SendEmailComandHandler()
		{
		}

		protected override Task Handle(SendEmailComand request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
