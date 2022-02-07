using Agenda_AspNet.Data;
using Agenda_AspNet.Models;
using Agenda_AspNet_Api.Business.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet_Api.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly Context _context;
        public CategoriaRepository(Context context)
        {
            _context = context;
        }
        public async void Adicionar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public IList<Categoria> ObterCategorias()
        {
            return _context.Categorias.ToList();
        }
        public async Task<Categoria> ObterCategoriaId(int CategoriaId)
        {
            return await _context.Categorias.Where(c => c.id == CategoriaId).FirstOrDefaultAsync();
        }
        public bool CategoriaExist(string descricao)
        {
            return _context.Categorias.Any(x => x.descricao == descricao);
        }
    }
}
