using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda_AspNet.Data;
using Agenda_AspNet.Models;
using Microsoft.AspNetCore.Authorization;

namespace Agenda_AspNet.Controllers
{
    [Authorize]
    public class EnderecoController : Controller
    {
        private readonly Context _context;

        public EnderecoController(Context context)
        {
            _context = context;
        }

        // GET: Endereco/Create
        public IActionResult Create()
        {
            ViewBag.contato_id = new SelectList(_context.Contatos, "id", "nomecompleto");
            return PartialView("_Create");
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cep,logradouro,numero,complemento,bairro,localidade,uf,contato_id")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                if(!EnderecoExists(endereco.cep, endereco.contato_id))
                {
                    _context.Add(endereco);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Endereço cadastrado!";
                    return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
                }
                TempData["error"] = "Este endereço já encontrasse cadastrado!";
                return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
            }
            TempData["warning"] = "Endereço Invalido!";
            return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
        }

        // GET: Endereco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Endereço não localizado!";
                return RedirectToRoute(new { controller = "Contato", action = "Index" });
            }

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                TempData["error"] = "Endereço não localizado!";
                return RedirectToRoute(new { controller = "Contato", action = "Index" });
            }
            ViewBag.contato_id = new SelectList(_context.Contatos, "id", "nomecompleto", endereco.contato_id);
            return PartialView("_Edit", endereco);
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,cep,logradouro,numero,complemento,bairro,localidade,uf,contato_id")] Endereco endereco)
        {
            if (id != endereco.id)
            {
                TempData["error"] = "Endereço não localizado!";
                return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endereco);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Endereço Atualizado!";
                    return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.cep, endereco.contato_id))
                    {
                        TempData["error"] = "Endereço não localizado!";
                        return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            TempData["warning"] = "Endereço não é valido!";
            return RedirectToRoute(new { controller = "Contato", action = "Details", id = endereco.contato_id });
        }

        // GET: Endereco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "Endereço não localizado!";
                return RedirectToRoute(new { controller = "Contato", action = "Index" });
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.id == id);
            if (endereco == null)
            {
                TempData["error"] = "Endereço não localizado!";
                return RedirectToRoute(new { controller = "Contato", action = "Index" });
            }
            endereco.contato = await _context.Contatos.FindAsync(endereco.contato_id);
            return PartialView("_Delete", endereco);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            int contato_id = endereco.contato_id;
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            TempData["success"] = "Endereço excluído!";
            return RedirectToRoute(new { controller = "Contato", action = "Details", id = contato_id });
        }

        private bool EnderecoExists(int cep, int contato)
        {
            return _context.Enderecos.Any(e => e.cep == cep && e.contato_id == contato);
        }
    }
}
