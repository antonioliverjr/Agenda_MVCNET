﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Models
{
    public class Contato
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Primeiro Nome")]
        public string nome { get; set; }
        [Required]
        [Display(Name = "Sobrenome")]
        public string sobrenome { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Display(Name = "Data Cadastro")]
        public DateTime data_criacao { get; set; } = DateTime.Now;
        [Display(Name = "Descrição do Contato")]
        public string descricao { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public int categoria_id { get; set; }
        [ForeignKey("categoria_id")]
        [Display(Name = "Categoria")]
        public Categoria categoria { get; set; }
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