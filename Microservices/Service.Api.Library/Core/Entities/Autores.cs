using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Service.Api.Library.Core.Entities
{
    public class Autores
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("academicDegree")]
        public string AcademicDegree { get; set; }
    }
}
