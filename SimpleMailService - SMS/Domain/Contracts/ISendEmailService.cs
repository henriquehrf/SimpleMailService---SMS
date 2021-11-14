using FluentEmail.Core.Models;
using SimpleMailService___SMS.Domain.Models;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Domain.Contracts
{
	public interface ISendEmailService
	{
		public Task<SendResponse> Send(EmailBase emailCommand);
	}
}
