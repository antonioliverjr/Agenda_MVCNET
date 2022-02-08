using System.ComponentModel.DataAnnotations;

namespace Agenda_AspNet_Api.Models.Account
{
    public class LoginInputViewModel
    {
		[Display(Name = "Usuário")]
		[Required(ErrorMessage = "O campo de usuário é obrigatório.")]
		public string UserName { get; set; }
		[Display(Name = "Senha")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "O campo password é obrigatório.")]
		public string Password { get; set; }
	}
}
