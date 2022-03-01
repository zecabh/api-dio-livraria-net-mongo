using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace livrariaDIOAPI.Models
{
    public class LivroDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _Id { get; set; }

        public string NomeLivro { get; set; }

        public double Preco { get; set; }

        public int Quantidade{ get; set; }

        public string Categoria { get; set; }

        public string Img { get; set; }

        public string _IdEditora { get; set; }

        public virtual EditoraDTO Editora { get; set; }
    }
}
