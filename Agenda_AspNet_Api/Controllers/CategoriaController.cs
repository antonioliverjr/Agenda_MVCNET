using Agenda_AspNet.Models;
using Agenda_AspNet_Api.Business.Repository;
using Agenda_AspNet_Api.Models.Categoria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Agenda_AspNet_Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaController(ICategoriaRepository categoriaRepository, ILogger<CategoriaController> logger)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
        }
        [SwaggerResponse(200, "Categorias retornadas com sucesso.", Type = typeof(IList<Categoria>))]
        [SwaggerResponse(204, "Sem Categorias cadastradas.", Type = null)]
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IList<Categoria>> GetAll()
        {
            var categorias = _categoriaRepository.ObterCategorias();
            if (categorias.Equals(null))
            {
                return NoContent();
            }
            return Ok(categorias);
        }
        [SwaggerResponse(200, "Categoria retornada com sucesso.", Type = typeof(CategoriaOutputViewModel))]
        [SwaggerResponse(400, "Id da Categoria incorreta ou inexistente.", Type = null)]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<CategoriaOutputViewModel> Get(int id)
        {
            var categoria = _categoriaRepository.ObterCategoriaId(id);
            if (categoria.Result == null)
            {
                return BadRequest("Categoria Inexistente.");
            }
            CategoriaOutputViewModel categoriaOutput = new CategoriaOutputViewModel();
            categoriaOutput.descricao = categoria.Result.descricao.ToString();
            return Ok(categoriaOutput);
        }
        [SwaggerResponse(201, "Categoria cadastrada com sucesso.", Type = typeof(Categoria))]
        [SwaggerResponse(400, "Descrição Categoria incorreta ou já existente.", Type = null)]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post(CategoriaInputViewModel categoriaInput)
        {
            if (ModelState.IsValid)
            {
                if (_categoriaRepository.CategoriaExist(categoriaInput.descricao))
                {
                    return BadRequest("Categoria Existente, favor inserir uma nova descrição.");
                }
                Categoria categoria = new Categoria();
                categoria.descricao = categoriaInput.descricao;
                _categoriaRepository.Adicionar(categoria);
                _categoriaRepository.Commit();

                return Created("", categoria);
            }
            return BadRequest(ModelState.Values);
        }
    }
}
