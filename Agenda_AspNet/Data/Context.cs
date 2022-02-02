using Agenda_AspNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Agenda_AspNet.Data
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        /*public Context(DbContextOptions<Context> options) : base(options)
        {
        }*/
        
        private readonly IConfiguration _configuration;
        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }

        public virtual DbSet<Contato> Contatos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
    }
}
