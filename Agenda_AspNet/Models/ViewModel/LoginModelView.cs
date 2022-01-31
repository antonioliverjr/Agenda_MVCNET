using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Models.ViewModel
{
    public class LoginModelView
    {
        [Display(Name = "Usuário")]
        //[EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [Required(ErrorMessage = "O campo de usuário é obrigatório.")]
        public string UserName { get; set; }
        [Display(Name = "Senha")]
        [DataType(DataType.Password )]
        [Required(ErrorMessage = "O campo password é obrigatório.")]
        public string Password { get; set; }
        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
