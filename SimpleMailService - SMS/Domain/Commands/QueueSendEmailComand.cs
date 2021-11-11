using System;

namespace SimpleMailService___SMS.Domain.Commands
{
	public sealed class QueueSendEmailComand : SendEmailComand
	{
		public DateTime RegistredAt { get; set; }
	}
}
