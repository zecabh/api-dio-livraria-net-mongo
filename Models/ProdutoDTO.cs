using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace livrariaDIOAPI.Models
{
    public class ProdutoDTO
    {
        [Key]
        public string _Id { get; set; }

        public string Nome { get; set; }

        public double Preco { get; set; }

        public int Quantidade{ get; set; }

        public string Categoria { get; set; }

        public string Img { get; set; }
    }
}
