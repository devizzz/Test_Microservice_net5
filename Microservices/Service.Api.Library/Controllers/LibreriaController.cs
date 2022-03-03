using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Api.Library.Core.Entities;
using Service.Api.Library.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Api.Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaController : Controller
    {
        // repositorio de auto
        private readonly IAutoresRepository _IAutoresRepository;

        // repositorio generico
        private readonly IMongoRepository<AutorEntity> _AutorEntity;

        public LibreriaController(IAutoresRepository _IAutoresRepository, IMongoRepository<AutorEntity> _AutorEntity)
        {
            this._IAutoresRepository = _IAutoresRepository;
            this._AutorEntity = _AutorEntity;
        }

        // usamos un repositorio propio
        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autores>>> GetAutores()
        {
            var autores = await _IAutoresRepository.GetAutores();
            return Ok(autores);
        }

        // usamos el repositorio generico
        [HttpGet("autoresGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutoresGenerico()
        {
            var autores = await _AutorEntity.GetAll();
            return Ok(autores);
        }
    }
}
