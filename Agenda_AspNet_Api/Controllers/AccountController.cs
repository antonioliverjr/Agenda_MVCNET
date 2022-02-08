using Agenda_AspNet.Models.Interface;
using Agenda_AspNet_Api.Business.Repository;
using Agenda_AspNet_Api.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Agenda_AspNet_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
		public readonly IAccountRepository _account;
		public readonly IAuthenticationService _authenticationService;
		public AccountController(IAccountRepository account, IAuthenticationService authenticationService)
		{
			_account = account;
			_authenticationService = authenticationService;
		}
		[HttpPost]
		[Route("Token")]
		public async Task<IActionResult> Login(LoginInputViewModel loginInput)
		{
			var user = await _account.ObterUser(loginInput.UserName, loginInput.Password);
			if (user.Equals(null))
			{
				return BadRequest("Usuário e Senha não conferem.");
			}
			User usuario = new User();
			usuario.Codigo = user.Id;
			usuario.UserName = user.UserName;
			usuario.Email = user.Email;

			var token = _authenticationService.GerarToken(usuario);

			return Ok(new { Token = token, Usuario = usuario });
		}
	}
}
