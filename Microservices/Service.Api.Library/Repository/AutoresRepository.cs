using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Service.Api.Library.Core.ContextMongoDB;
using Service.Api.Library.Core.Entities;

namespace Service.Api.Library.Repository
{
    public class AutoresRepository : IAutoresRepository
    {
        private readonly IAutoresContext _IAutoresContext;

        public AutoresRepository(IAutoresContext _IAutoresContext)
        {
            this._IAutoresContext = _IAutoresContext;
        }

        public async Task<IEnumerable<Autores>> GetAutores()
        {
            //var filter = Builders<Autores>.Filter.Eq("UserName", "");
            return await _IAutoresContext.Autores.Find(a => true).ToListAsync();
        }
    }
}
