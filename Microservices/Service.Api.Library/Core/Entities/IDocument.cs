using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Api.Library.Core.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        DateTime CreateDate { get; }
    }
}
