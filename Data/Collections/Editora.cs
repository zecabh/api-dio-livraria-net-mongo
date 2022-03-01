using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livrariaDIOAPI.Data.Collections
{
    public class Editora
    {
        public Editora(string id, string nomeEditora)
        {
            this._IdEditora = id;
            this.NomeEditora = nomeEditora;
           // this.Livros = livros;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _IdEditora { get; set; }

        public string NomeEditora { get; set; }

        // public ICollection<Livro> Livros { get; set; }

    }
}
