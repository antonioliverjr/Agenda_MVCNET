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
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

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
        public async Task<IActionResult> Index(string? filter, int? page)
        {
            int pageSize = 15;
            var contatos = _context.Contatos.AsNoTracking().AsQueryable().OrderBy(c => c.nome);

            if (filter != null && ContatoSearchExists(filter))
            {
                contatos = contatos.Where(c => c.nome.Contains(filter) || c.sobrenome.Contains(filter) || c.telefone.Contains(filter))
                    .OrderBy(c => c.nome);
            }    
            
            var resultado = await PagingList.CreateAsync(contatos, pageSize, page ?? 1);
            
            if (filter != null && ContatoSearchExists(filter))
            {
                ViewBag.filter = filter;
                resultado.RouteValue = new RouteValueDictionary();
                resultado.RouteValue.Add("filter", filter);
            }
            else if (filter != null)
            {
                TempData["error"] = "Nome ou Telefone pesquisado não existe!";
            }

            return View(resultado);
        }

        // GET: Contato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Contato Inexistente!";
                return RedirectToAction(nameof(Index));
            }

            var contato = await _context.Contatos
                .FirstOrDefaultAsync(m => m.id == id);
            if (contato == null)
            {
                TempData["error"] = "Contato Inexistente";
                return RedirectToAction(nameof(Index));
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
                        TempData["info"] = $"Foto salva para o Contato {contato.nome}";
                    }
                    TempData["success"] = "Contato Adicionado!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["error"] = "Contato Inexistente";
                    return RedirectToAction(nameof(Index));
                }
                
            }
            ViewBag.categoria_id = new SelectList(_context.Categorias, "id", "descricao", contato.categoria_id);
            TempData["warning"] = "Dados do formulário incorretos!";
            return View(contato);
        }

        // GET: Contato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Contato Inexistente";
                return RedirectToAction(nameof(Index));
            }

            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
            {
                TempData["error"] = "Contato Inexistente";
                return RedirectToAction(nameof(Index));
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
                TempData["error"] = "Contato Inexistente";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (foto_input != null)
                    {
                        var file = AddFileUpload(foto_input, contato.id);
                        contato.foto = file.ToString();
                        TempData["info"] = "Foto atualizada!";
                    }

                    _context.Update(contato);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Contato Atualizado!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExists(contato.id))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.categoria_id = new SelectList(_context.Categorias, "id", "descricao", contato.categoria_id);
            TempData["warning"] = "Dados do formulário incorretos!";
            return View(contato);
        }

        // GET: Contato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Contato Inexistente";
                return RedirectToAction(nameof(Index));
            }

            var contato = await _context.Contatos
                .FirstOrDefaultAsync(m => m.id == id);
            if (contato == null)
            {
                TempData["error"] = "Contato Inexistente";
                return RedirectToAction(nameof(Index));
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
                TempData["success"] = "Contato Excluído!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Contato Inexistente";
            return RedirectToAction(nameof(Index));
        }

        private bool ContatoExists(int id)
        {
            return _context.Contatos.Any(e => e.id == id);
        }

        private bool ContatoSearchExists(string? filter)
        {
            return _context.Contatos.Any(c => c.nome.Contains(filter) || c.sobrenome.Contains(filter) || c.telefone.Contains(filter));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
