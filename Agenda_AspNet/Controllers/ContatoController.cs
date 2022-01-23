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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Agenda_AspNet.Controllers
{
    public class ContatoController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<ContatoController> _logger;
        private readonly IWebHostEnvironment _env;

        public ContatoController(Context context, ILogger<ContatoController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        // GET: Contato
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contatos.ToListAsync());
        }

        // GET: Contato/Search/String
        public async Task<IActionResult> Search(string? termo)
        {
            if (termo == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var contatos = await _context.Contatos
                .Where(c => c.nome.Contains(termo) || c.sobrenome.Contains(termo) || c.telefone.Contains(termo))
                .ToListAsync();

            ViewBag.termo = termo;
            return PartialView("Index", contatos);
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


        public string AddFileUpload(IFormFile foto, int id_contato)
        {
            var dir = _env.WebRootPath + "\\media\\foto\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (foto != null)
            {
                var extensao = Path.GetExtension(foto.FileName);
                if (extensao.Contains(".jpg") || extensao.Contains(".png") || extensao.Contains(".jpeg"))
                {
                    var arquivo_antigo = Directory.GetFiles(dir, id_contato + ".*");
                    if (arquivo_antigo != null)
                    {
                        foreach(string file in arquivo_antigo)
                        {
                            if (Path.GetExtension(file) != extensao)
                            {
                                FileInfo arq = new FileInfo(Path.Combine(dir, file));
                                arq.Delete();
                            }
                        }
                    }
                    var nome_arquivo = Path.Combine(dir, id_contato + extensao);
                    var nome_banco = "~/media/foto/" + id_contato + extensao;
                    using (var fileStream = new FileStream(nome_arquivo, FileMode.Create, FileAccess.Write))
                    {
                        foto.CopyTo(fileStream);
                        return nome_banco;
                    }
                }                
            }

            return null;
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
        public async Task<IActionResult> Create([Bind("nome,sobrenome,telefone,email,descricao,categoria_id,foto")] Contato contato, IFormFile foto_input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(contato);
                    await _context.SaveChangesAsync();
                    var file = AddFileUpload(foto_input, contato.id);
                    if (file != null)
                    {
                        contato.foto = file.ToString();
                        _context.Update(contato);
                        await _context.SaveChangesAsync();
                    }
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
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,sobrenome,telefone,email,data_criacao,descricao,categoria_id,foto,ativo")] Contato contato, IFormFile foto_input)
        {
            if (id != contato.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (foto_input != null)
                    {
                        var file = AddFileUpload(foto_input, contato.id);
                        contato.foto = file.ToString();
                    }

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
