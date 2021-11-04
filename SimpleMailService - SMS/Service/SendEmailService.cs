using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using SimpleMailService___SMS.Domain.Commands;
using SimpleMailService___SMS.Domain.Contracts;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Service
{
	public class SendEmailService : ISendEmailService
	{
		private IConfiguration _configuration;

		public SendEmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<SendResponse> Send(SendEmailComand emailCommand)
		{
			using var smtp = new SmtpClient
			{
				Host = _configuration.GetValue<string>("MailBoxConfig:Host"),
				Port = _configuration.GetValue<int>("MailBoxConfig:587"),
				EnableSsl = _configuration.GetValue<bool>("MailBoxConfig:EnableSsl"),
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(_configuration.GetValue<string>("MailBoxConfig:Username"),
													_configuration.GetValue<string>("MailBoxConfig:Password"))
			};

			Email.DefaultSender = new SmtpSender(smtp);

			var sender = Email.From(emailCommand.From)
							  .To(emailCommand.To)
							  .Subject(emailCommand.Subject)
							  .Body(emailCommand.Body, emailCommand.BodyIsHtml);

			return await sender.SendAsync();
		}
	}
}
