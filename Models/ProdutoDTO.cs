using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace livrariaDIOAPI.Models
{
    public class ProdutoDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _Id { get; set; }

        public string Nome { get; set; }

        public double Preco { get; set; }

        public int Quantidade{ get; set; }

        public string Categoria { get; set; }

        public string Img { get; set; }
    }
}
