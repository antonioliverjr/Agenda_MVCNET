using System.ComponentModel.DataAnnotations;

namespace Agenda_AspNet.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Usuário")]
        [StringLength(256, ErrorMessage = "Usuário deve ter no mínimo 8 digitos, sem espaços e simbolos.", MinimumLength = 8)]
        [Required(ErrorMessage = "Informe um nome de usuário.")]
        public string UserName{ get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        public string Email { get; set; }
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "A senha deve conter no mínimo 6 digitos", MinimumLength = 6)]
        [Required(ErrorMessage = "O campo password é obrigatório.")]
        public string Password { get; set; }
        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "A senha deve conter no mínimo 6 digitos", MinimumLength = 6)]
        [Required(ErrorMessage = "É necessário confirmar a senha.")]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
