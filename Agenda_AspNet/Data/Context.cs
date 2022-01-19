using Agenda_AspNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Data
{
    public class Context : DbContext
    {
        //private readonly IConfiguration configuration;
        public virtual DbSet<Contato> Contatos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DELLJR\\SQLEXPRESS;Database=AGENDA;User ID=project;Password=Jrdbsql");
        }
    }
}
