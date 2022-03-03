
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Api.Library.Core.Entities
{
    [BsonCollection("Autores")]
    public class AutorEntity : Document
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("academicDegree")]
        public string AcademicDegree { get; set; }
    }
}
