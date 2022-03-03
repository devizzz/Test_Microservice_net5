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
    public class LibroController : Controller
    {
        private readonly IMongoRepository<LibroEntity> _libroEntity;

        public LibroController(IMongoRepository<LibroEntity> _libroEntity)
        {
            this._libroEntity = _libroEntity;
        }

        [HttpPost("Create")]
        public async Task CreateBook([FromBody] LibroEntity book)
        {
            await _libroEntity.InsertDocument(book);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<LibroEntity>>> GetAll()
        {
            return Ok(await _libroEntity.GetAll());
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<List<LibroEntity>>> GetById(string id)
        {
            return Ok(await _libroEntity.GetById(id));
        }

        [HttpPost("Pagination")]
        public async Task<ActionResult<List<LibroEntity>>> Pagination([FromBody] PaginationEntity<LibroEntity> pagination)
        {
            return Ok(await _libroEntity.PaginationBy2(pagination));
        }
    }
}
