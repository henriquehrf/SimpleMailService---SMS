using MediatR;

namespace SimpleMailService___SMS.Domain.Commands
{
	public class SendEmailComand : IRequest
	{
		public string From { get; set; }
		public string To { get; set; }
		//public string CopyTo { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public bool BodyIsHtml { get; set; }
	}
}
