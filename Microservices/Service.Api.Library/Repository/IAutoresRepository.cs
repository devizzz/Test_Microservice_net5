using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Api.Library.Core.Entities;

namespace Service.Api.Library.Repository
{
    public interface IAutoresRepository
    {
        Task<IEnumerable<Autores>> GetAutores();
    }
}
