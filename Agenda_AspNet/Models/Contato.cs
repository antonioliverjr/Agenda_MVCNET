using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda_AspNet.Models
{
    public class Contato
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O Primeiro Nome é obrigatório.")]
        [Display(Name = "Primeiro Nome")]
        public string nome { get; set; }
        [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
        [Display(Name = "Sobrenome")]
        public string sobrenome { get; set; }
        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Display(Name = "Data Cadastro")]
        public DateTime data_criacao { get; set; } = DateTime.Now;
        [Display(Name = "Descrição do Contato")]
        public string descricao { get; set; }
        [Required(ErrorMessage = "Selecione uma Categoria.")]
        [Display(Name = "Categoria")]
        public int categoria_id { get; set; }
        [ForeignKey("categoria_id")]
        [Display(Name = "Categoria")]
        public Categoria categoria { get; set; }
        [Display(Name = "Foto Perfil")]
        public string foto { get; set; }
        [Display(Name = "Status")]
        public bool ativo { get; set; } = true;
        [Display(Name = "Endereços")]
        public List<Endereco> enderecos { get; set; }

        public string nomecompleto
        { 
            get 
            {
                return string.Format("{0} {1}", this.nome, this.sobrenome);
            } 
        }
    }
}
