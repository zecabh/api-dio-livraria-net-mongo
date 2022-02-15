using livrariaDIOAPI.Data.Collections;
using System.Threading.Tasks;
using livrariaDIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;

namespace livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrariaController : ControllerBase
    {
        livrariaDIOAPI.Data.MongoDB _mongoDB;
        IMongoCollection<Produto> _produtosCollection;

        public LivrariaController(livrariaDIOAPI.Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _produtosCollection = _mongoDB.DB.GetCollection<Produto>(typeof(Produto).Name.ToLower());
        }


        [HttpPost]
        //public async Task<ActionResult> InserirProduto([FromBody] Produto prod)
        
        public ActionResult InserirProduto([FromBody] ProdutoDTO prod)
        {

            var produto =  new Produto(prod._Id, prod.Nome, prod.Preco, prod.Quantidade, prod.Categoria, prod.Img);

            _produtosCollection.InsertOne(produto);

            return StatusCode(201, "Livro adicionado com sucesso!");
        }



        // GET: api/Livraria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _produtosCollection.Find(Builders<Produto>.Filter.Empty).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoPorId(string id)
        {

            var filter = Builders<Produto>.Filter.Eq(x => x.Id, id);

            var produto = await _produtosCollection.Find(filter).SingleAsync();

            if (produto == null) 
            {
                return NotFound();
            }

            return produto;
        }


        // DELETE: api/Livraria
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(string id)
        {

            var filter = Builders<Produto>.Filter.Eq(x => x.Id, id);

            var produto = await _produtosCollection.Find(filter).SingleAsync();

            if (produto == null)
            {
                return NotFound();
            }

            _produtosCollection.DeleteOne(filter);

            return StatusCode(201, "Livro excluido com sucesso!");
        }

    }
}
