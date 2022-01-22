using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Models
{
    public class Endereco
    {
        [Display(Name = "Registro")]
        public int id { get; set; }
        [Required]
        [Display(Name = "Cep")]
        [MaxLength(8)]
        public int cep { get; set; }
        [Required]
        [Display(Name = "Endereço")]
        public string logradouro { get; set; }
        [Required]
        [Display(Name = "Numero")]
        public string numero { get; set; }
        [Display(Name = "Complemento")]
        public string complemento { get; set; }
        [Required]
        [Display(Name = "Bairro")]
        public string bairro { get; set; }
        [Required]
        [Display(Name = "Cidade")]
        public string localidade { get; set; }
        [Required]
        [Display(Name = "Estado")]
        [StringLength(2, ErrorMessage = "Para Estado informe a uf apenas, Ex: Bahia = BA")]
        public string uf { get; set; }
        [Required]
        [Display(Name = "Contato")]
        public int contato_id { get; set; }
        [ForeignKey("nome_contato_id")]
        public Contato contato { get; set; }
    }
}
