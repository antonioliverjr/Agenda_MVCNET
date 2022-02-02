using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Agenda_AspNet.Data;
using Agenda_AspNet.Models;
using Microsoft.AspNetCore.Authorization;

namespace Agenda_AspNet.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly Context _context;

        public CategoriaController(Context context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("descricao")] Categoria categoria)
        {
            if (ModelState.IsValid && !CategoriaExists(categoria.descricao))
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["success"] = "Cadastro realizado!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Erro! Descrição inválida ou já utilizada!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Categoria inexistente!";
                return RedirectToAction(nameof(Index));
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                TempData["error"] = "Categoria inexistente!";
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_Edit", categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,descricao")] Categoria categoria)
        {
            if (id != categoria.id)
            {
                TempData["error"] = "Categoria inexistente!";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid && !CategoriaExists(categoria.descricao))
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Categoria Atualizada!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.descricao))
                    {
                        TempData["error"] = "Categoria inexistente!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["warning"] = "Categoria Existente ou Descrição invalida!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Categoria inexistente!";
                return RedirectToAction(nameof(Index));
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.id == id);
            if (categoria == null)
            {
                TempData["error"] = "Categoria inexistente!";
                return RedirectToAction(nameof(Index));
            }
            
            return PartialView("_Delete", categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["success"] = "Categoria Excluida!";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(string descricao)
        {
            return _context.Categorias.Any(e => e.descricao == descricao);
        }
    }
}
