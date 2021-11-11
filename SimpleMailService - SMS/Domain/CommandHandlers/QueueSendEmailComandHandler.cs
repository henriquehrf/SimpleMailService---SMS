using MassTransit;
using MediatR;
using SimpleMailService___SMS.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Domain.CommandHandlers
{
	public class QueueSendEmailComandHandler : AsyncRequestHandler<QueueSendEmailComand>
	{
		private readonly IBus _bus;

		public QueueSendEmailComandHandler(IBus bus)
		{
			_bus = bus;
		}

		protected async override Task Handle(QueueSendEmailComand request, CancellationToken cancellationToken)
		{
			request.RegistredAt = DateTime.Now;

			var uri = new Uri("rabbitmq://localhost/sheduleQueue");
			var endPoint = await _bus.GetSendEndpoint(uri);
			await endPoint.Send(request);

		}
	}
}
