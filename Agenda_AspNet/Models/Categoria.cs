using System.ComponentModel.DataAnnotations;

namespace Agenda_AspNet.Models
{
    public class Categoria
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public string descricao { get; set; }
    }
}
