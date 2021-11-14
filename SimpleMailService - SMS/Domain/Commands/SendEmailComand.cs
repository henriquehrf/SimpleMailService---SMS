using FluentEmail.Core;
using MediatR;
using SimpleMailService___SMS.Domain.Models;

namespace SimpleMailService___SMS.Domain.Commands
{
	public class SendEmailComand : EmailBase, IRequest
	{
		public string Body { get; set; }
		public bool IsHtml { get; set; }

		public override IFluentEmail GetFluentEmailConfig()
		{
			return GetFluentEmailConfigGlobal()
					.Body(Body, IsHtml);
		}
	}
}
