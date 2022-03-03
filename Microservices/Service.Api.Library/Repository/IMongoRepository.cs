using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Service.Api.Library.Core.Entities;

namespace Service.Api.Library.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();
        Task<TDocument> GetById(string Id);
        Task InsertDocument(TDocument document);
        Task UpdateDocument(TDocument document);
        Task DeleteById(string Id);

        // Filtro 1
        Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument, bool>> filterExpression, PaginationEntity<TDocument> pagination);

        // Filtro 2
        Task<PaginationEntity<TDocument>> PaginationBy2(PaginationEntity<TDocument> pagination);
    }
}
