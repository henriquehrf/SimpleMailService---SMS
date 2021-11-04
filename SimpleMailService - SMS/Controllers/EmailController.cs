using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SimpleMailService___SMS.Controllers
{
	[ApiController]
	[Route("api/emails")]
	public class EmailController : ControllerBase
	{
		[HttpPost("send")]
		public async Task<IActionResult> Send()
		{
			return Ok();
		}
	}
}
