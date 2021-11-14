using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using SimpleMailService___SMS.Domain.Contracts;
using SimpleMailService___SMS.Domain.Models;
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

		public async Task<SendResponse> Send(EmailBase emailCommand)
		{
			using var smtp = new SmtpClient
			{
				Host = _configuration.GetValue<string>("MailBoxConfig:Host"),
				Port = _configuration.GetValue<int>("MailBoxConfig:Port"),
				EnableSsl = _configuration.GetValue<bool>("MailBoxConfig:EnableSsl"),
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(_configuration.GetValue<string>("MailBoxConfig:Username"),
													_configuration.GetValue<string>("MailBoxConfig:Password")),
			};

			Email.DefaultSender = new SmtpSender(smtp);
			Email.DefaultRenderer = new RazorRenderer();

			IFluentEmail sender = emailCommand.GetFluentEmailConfig();

			return await sender.SendAsync();
		}
	}
}
