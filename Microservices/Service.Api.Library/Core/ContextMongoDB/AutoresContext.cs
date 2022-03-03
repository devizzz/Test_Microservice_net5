using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Service.Api.Library.Core.Entities;

namespace Service.Api.Library.Core.ContextMongoDB
{
    public class AutoresContext : IAutoresContext
    {
        private readonly IMongoDatabase _db;

        public AutoresContext(IOptions<MongoSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Autores> Autores => _db.GetCollection<Autores>("Autores");
    }
}
