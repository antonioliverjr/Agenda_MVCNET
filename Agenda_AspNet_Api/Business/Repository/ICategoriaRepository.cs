using Agenda_AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet_Api.Business.Repository
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        void Commit();
        IList<Categoria> ObterCategorias();
        Task<Categoria> ObterCategoriaId(int CategoriaId);
        bool CategoriaExist(string descricao);
    }
}
