using System;
using MongoDB.Driver;
using Service.Api.Library.Core.Entities;

namespace Service.Api.Library.Core.ContextMongoDB
{
    public interface IAutoresContext
    {
        IMongoCollection<Autores> Autores { get; }
    }
}
