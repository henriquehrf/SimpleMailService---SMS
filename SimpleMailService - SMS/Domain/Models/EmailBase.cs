using FluentEmail.Core;
using FluentEmail.Core.Models;
using SimpleMailService___SMS.Domain.Enums;
using System.Collections.Generic;

namespace SimpleMailService___SMS.Domain.Models
{
	public abstract class EmailBase
	{
		public string From { get; set; }
		public string To { get; set; }
		public List<Address> CopyTo { get; set; }
		public List<Address> ReplyTo { get; set; }
		public string Subject { get; set; }
		public string Tag { get; set; }
		public PriorityEmailEnum Priority { get; set; }
		public List<Attachment> Attachments { get; set; }

		protected IFluentEmail GetFluentEmailConfigGlobal()
		{
			return Email.From(From)
						.To(To)
						.Subject(Subject)
						.CC(CopyTo);
		}

		public abstract IFluentEmail GetFluentEmailConfig();
	}

}
