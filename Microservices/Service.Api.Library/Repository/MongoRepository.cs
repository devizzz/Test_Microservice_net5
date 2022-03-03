using System;
using Service.Api.Library.Core.Entities;
using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Service.Api.Library.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace Service.Api.Library.Repository
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);
            IMongoDatabase _db = client.GetDatabase(options.Value.Database);

            _collection = _db.GetCollection<TDocument>(CollectionName(typeof(TDocument)));
        }

        private protected string CollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.Find(p => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string Id)
        {
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task UpdateDocument(TDocument document)
        {
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public async Task DeleteById(string Id)
        {
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, Id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument, bool>> filterExpression, PaginationEntity<TDocument> pagination)
        {
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);
            if (pagination.SortDirection.ToLower() == "desc")
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);

            if (string.IsNullOrEmpty(pagination.Filter))
            {
                pagination.Data = await _collection.Find(p => true)
                    .Sort(sort)
                    .Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();
            }
            else
                pagination.Data = await _collection.Find(filterExpression)
                    .Sort(sort)
                    .Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();

            long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty);
            var totalPages = Convert.ToInt32(Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.PageSize)));

            pagination.PagesQuantity = totalPages;

            return pagination;
        }

        public async Task<PaginationEntity<TDocument>> PaginationBy2(PaginationEntity<TDocument> pagination)
        {
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);
            if (pagination.SortDirection.ToLower() == "desc")
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);

            long totalDocuments = 0;

            if (pagination.FilterValue == null || string.IsNullOrEmpty(pagination.FilterValue.Value))
            {
                pagination.Data = await _collection.Find(p => true)
                    .Sort(sort)
                    .Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();

                totalDocuments = (await _collection.Find(p => true).ToListAsync()).Count();
            }
            else
            {
                var valueFilter = $".*{pagination.FilterValue.Value}.*";
                var filter = Builders<TDocument>.Filter.Regex(pagination.FilterValue.Property, new BsonRegularExpression(valueFilter, "i"));

                pagination.Data = await _collection.Find(filter)
                    .Sort(sort)
                    .Skip((pagination.Page - 1) * pagination.PageSize)
                    .Limit(pagination.PageSize).ToListAsync();

                totalDocuments = (await _collection.Find(filter).ToListAsync()).Count();
            }

            var totalPages = Convert.ToInt32(Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.PageSize)));

            pagination.PagesQuantity = totalPages;
            pagination.TotalRows = Convert.ToInt32(totalDocuments);

            return pagination;
        }
    }
}
