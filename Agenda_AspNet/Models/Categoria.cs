using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
