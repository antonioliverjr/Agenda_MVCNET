using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda_AspNet.Data;
using Agenda_AspNet.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Agenda_AspNet.Controllers
{
    public class ContatoController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<ContatoController> _logger;

        public ContatoController(Context context, ILogger<ContatoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Contato
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contatos.ToListAsync());
        }

        // GET: Contato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos
                .FirstOrDefaultAsync(m => m.id == id);
            if (contato == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(contato.categoria_id);
            contato.categoria = categoria;

            List<Endereco> enderecos = await _context.Enderecos
                .Where(x => x.contato_id == contato.id)
                .ToListAsync();
            contato.enderecos = enderecos;

            return View(contato);
        }

        // GET: Contato/Create
        public IActionResult Create()
        {
            ViewBag.categoria_id = new SelectList(_context.Categorias, "id", "descricao");
            return View();
        }

        // POST: Contato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,sobrenome,telefone,email,descricao,categoria_id,foto")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(contato);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return NotFound();
                }
                
            }
            ViewBag.categoria_id = new SelectList(_context.Categorias, "id", "descricao", contato.categoria_id);
            return View(contato);
        }

        // GET: Contato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            ViewBag.categoria_id = new SelectList(_context.Categorias, "id", "descricao", contato.categoria_id);
            return View(contato);
        }

        // POST: Contato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,sobrenome,telefone,email,data_criacao,descricao,categoria_id,foto,ativo")] Contato contato)
        {
            if (id != contato.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExists(contato.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.categoria_id = new SelectList(_context.Categorias, "id", "descricao", contato.categoria_id);
            return View(contato);
        }

        // GET: Contato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos
                .FirstOrDefaultAsync(m => m.id == id);
            if (contato == null)
            {
                return NotFound();
            }

            return PartialView("_Delete", contato);
        }

        // POST: Contato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ContatoExists(id))
            {
                var contato = await _context.Contatos.FindAsync(id);
                _context.Contatos.Remove(contato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool ContatoExists(int id)
        {
            return _context.Contatos.Any(e => e.id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
