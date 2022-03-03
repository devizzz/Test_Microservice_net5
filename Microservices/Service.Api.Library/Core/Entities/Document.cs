using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Api.Library.Core.Entities
{
    public class Document : IDocument
    {
        public Document()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreateDate => DateTime.Now;
    }
}
