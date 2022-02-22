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
        public async Task<IActionResult> InserirProduto([FromBody] ProdutoDTO prod)
        {
            var produto =  new Produto(prod._Id, prod.Nome, prod.Preco, prod.Quantidade, prod.Categoria, prod.Img);

            await _produtosCollection.InsertOneAsync(produto);

            return StatusCode(201, "Livro adicionado com sucesso!");           
        }


        // GET: api/Livraria
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _produtosCollection.Find(Builders<Produto>.Filter.Empty).ToListAsync();

            return Ok(produtos);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Produto>> GetProdutoPorId(string id)
        {

            var filter = Builders<Produto>.Filter.Eq(x => x._Id, id);

            var produto = await _produtosCollection.Find(filter).FirstOrDefaultAsync();

            if (produto == null) 
            {
                return NotFound();
            }

            return produto;
        }


        // DELETE: api/Livraria
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProduto(string id)
        {
            var filter = Builders<Produto>.Filter.Eq(x => x._Id, id);

            var produto = await _produtosCollection.Find(filter).FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            await _produtosCollection.DeleteOneAsync(filter);

            return StatusCode(201, "Livro excluido com sucesso!");
        }  

    }
}
