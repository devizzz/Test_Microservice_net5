using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Api.Library.Core.Entities
{
    [BsonCollection("Libro")]
    public class LibroEntity : Document
    {
        [BsonElement("titulo")]
        public string Title { get; set; }

        [BsonElement("descripcion")]
        public string Description { get; set; }

        [BsonElement("precio")]
        public int Price { get; set; }

        [BsonElement("fechaPublicacion")]
        public DateTime? PublishDate { get; set; }

        [BsonElement("autor")]
        public AutorEntity Author { get; set; }
    }
}
