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
    public class LibreriaAutorController : Controller
    {
        private readonly IMongoRepository<AutorEntity> _AutorEntityRepository;

        public LibreriaAutorController(IMongoRepository<AutorEntity> _AutorEntityRepository)
        {
            this._AutorEntityRepository = _AutorEntityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> Get()
        {
            return Ok(await _AutorEntityRepository.GetAll());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> Get(string Id)
        {
            return Ok(await _AutorEntityRepository.GetById(Id));
        }

        [HttpPost]
        public async Task Put([FromBody] AutorEntity autorEntity)
        {
            await _AutorEntityRepository.InsertDocument(autorEntity);
        }

        [HttpPost("Pagination")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination([FromBody] PaginationEntity<AutorEntity> pagination)
        {
            return Ok(await _AutorEntityRepository.PaginationBy(
                filter => filter.Name == pagination.Filter,
                 pagination));
        }

        [HttpPost("Pagination2")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination2([FromBody] PaginationEntity<AutorEntity> pagination)
        {
            return Ok(await _AutorEntityRepository.PaginationBy2(pagination));
        }

        [HttpPut("{Id}")]
        public async Task Put(string Id, [FromBody] AutorEntity autorEntity)
        {
            autorEntity.Id = Id;
            await _AutorEntityRepository.UpdateDocument(autorEntity);
        }

        [HttpDelete("{Id}")]
        public async Task Delete(string Id)
        {
            await _AutorEntityRepository.DeleteById(Id);
        }
    }
}
