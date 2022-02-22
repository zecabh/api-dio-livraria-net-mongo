﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace livrariaDIOAPI.Data.Collections
{
    public class Produto
    {
        public Produto(string id, string nome, double preco, int quantidade, string categoria, string img)
        {
            this._Id = id;
            this.Nome = nome;
            this.Preco = preco;
            this.Quantidade = quantidade;
            this.Categoria = categoria;
            this.Img = img;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _Id { get; set; }

        public string Nome { get; set; }
        
        public double Preco { get; set; }

        public int Quantidade { get; set; }

        public string Categoria { get; set; }

        public string Img { get; set; }
    }
}
