using FluentEmail.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace SimpleMailService___SMS.Domain.Commands
{
	public class SendEmailComand : IRequest
	{
		public string From { get; set; }
		public string To { get; set; }
		public List<Address> CopyTo { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public bool BodyIsHtml { get; set; }
	}
}
