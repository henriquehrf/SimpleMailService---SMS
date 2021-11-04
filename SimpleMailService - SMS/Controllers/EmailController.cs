using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleMailService___SMS.Domain.Commands;
using SimpleMailService___SMS.Domain.Notification;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Controllers
{
	[ApiController]
	[Route("api/emails")]
	public class EmailController : ControllerBase
	{
		private readonly IMediator _bus;
		private readonly IDomainNotificationContext _notificationContext;

		public EmailController(IMediator bus, IDomainNotificationContext notificationContext)
		{
			_bus = bus;
			_notificationContext = notificationContext;
		}

		[HttpPost("send")]
		public async Task<IActionResult> Send(SendEmailComand comand)
		{
			await _bus.Send(comand);

			if (_notificationContext.HasErrorNotifications)
			{
				var notifications = _notificationContext.GetErrorNotifications();
				var message = string.Join(", ", notifications.Select(x => x.Value));
				return BadRequest(message);
			}

			return Ok();
		}
	}
}
