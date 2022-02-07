using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Agenda_AspNet_Api.Models.Categoria
{
    public class CategoriaInputViewModel
    {
        [JsonIgnore]
        public int id { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        [StringLength(50, ErrorMessage = "A descrição deve conter entre 5 a 50 caracteres.", MinimumLength = 5)]
        public string descricao { get; set; }
    }
}
