using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI.Models;
using NetCoreAPI.Repository;

namespace NetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly IRepository<Autor> _repository;

        public AutorController(IRepository<Autor> rep)
        {
            _repository = rep;
        }

        [HttpGet]
        public IEnumerable<Autor> GetAll()
        {
            return _repository.FindAll();
        }

        [HttpGet("{id}", Name = "GetAutor")]
        public IActionResult GetById(int id)
        {
            var Autor = _repository.Find(id);
            if (Autor == null)
            {
                return NotFound();
            }

            return new ObjectResult(Autor);
        }

        [HttpPost("{nomeCompleto}", Name = "PostAutor")]
        public IActionResult Create(string nomeCompleto)
        {
            if (nomeCompleto == null)
                return BadRequest();

            Autor _autor = new Autor(nomeCompleto);

            _repository.Add(_autor);

            return CreatedAtRoute("GetAutor", new { id = _autor.IdAutor }, _autor);
        }

        [HttpPost]
        public IActionResult CreateGroup([FromBody] List<string> lNomeCompleto)
        {
            foreach (var nomeCompleto in lNomeCompleto)
            {
                if (nomeCompleto == null)
                    return BadRequest();

                Autor _autor = new Autor(nomeCompleto);

                _repository.Add(_autor);
            }
            return new NoContentResult();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string nomeCompleto)
        {
            if (nomeCompleto == null)
                return BadRequest();

            var _autor = _repository.Find(id);
            Autor _autorAux = new Autor(nomeCompleto);

            if (_autor == null)
                return NotFound();

            _autor.Nome = _autorAux.Nome;
            _autor.NomeAux = _autorAux.NomeAux;
            _autor.NomeCompleto = _autorAux.NomeCompleto;
            _autor.NomeExibicao = _autorAux.NomeExibicao;
            _autor.Sobrenome = _autorAux.Sobrenome;

            _repository.Update(_autor);

            return new NoContentResult();


        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _autor = _repository.Find(id);

            if (_autor == null)
                return NotFound();

            _repository.Remove(id);
            return new NoContentResult();
        }
    }
}