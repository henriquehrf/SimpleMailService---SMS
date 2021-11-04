using FluentEmail.Core.Models;
using SimpleMailService___SMS.Domain.Commands;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Domain.Contracts
{
	public interface ISendEmailService
	{
		public Task<SendResponse> Send(SendEmailComand emailCommand);
	}
}
