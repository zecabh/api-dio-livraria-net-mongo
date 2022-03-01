using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livrariaDIOAPI.Models
{
    public class EditoraDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _IdEditora { get; set; }

        public string NomeEditora { get; set; }

       // public ICollection<LivroDTO> Livros { get; set; }

    }
}
