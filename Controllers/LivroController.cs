using livrariaDIOAPI.Data.Collections;
using livrariaDIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace livrariaDIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Livro> _produtosCollection;

        public LivroController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _produtosCollection = _mongoDB.DB.GetCollection<Livro>(typeof(Livro).Name.ToLower());
        }


        // POST api/<LivroController>
        [HttpPost]
        public async Task<IActionResult> InserirProduto([FromBody] LivroDTO livro)
        {
            var produto =  new Livro(livro._Id, livro.NomeLivro, livro.Preco, livro.Quantidade, livro.Categoria, livro.Img, livro._IdEditora);

            await _produtosCollection.InsertOneAsync(produto);           

            return StatusCode(201, "Livro adicionado com sucesso!");           
        }


        // GET: api/<LivroController>
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _produtosCollection.Find(Builders<Livro>.Filter.Empty).ToListAsync();

            return Ok(produtos);
        }

        // GET api/<LivroController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Livro>> GetProdutoPorId(string id)
        {

            var filter = Builders<Livro>.Filter.Eq(x => x._Id, id);

            var produto = await _produtosCollection.Find(filter).FirstOrDefaultAsync();

            if (produto == null) 
            {
                return NotFound();
            }

            return produto;
        }

        // PUT api/<LivroController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateProduto(string id, [FromBody] LivroDTO prod)
        {
            var filter = Builders<Livro>.Filter.Eq(x => x._Id, id);

            var livro = await _produtosCollection.Find(filter).FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound();
            }

            var updateProduto = new Livro(livro._Id, prod.NomeLivro, prod.Preco, prod.Quantidade, prod.Categoria, prod.Img, prod._IdEditora);

            await _produtosCollection.ReplaceOneAsync(filter, updateProduto);


            return StatusCode(200, "Livro alterado com sucesso!");
        }

        // DELETE api/<LivroController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteProduto(string id)
        {
            var filter = Builders<Livro>.Filter.Eq(x => x._Id, id);

            var produto = await _produtosCollection.Find(filter).FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            await _produtosCollection.DeleteOneAsync(filter);

            return StatusCode(200, "Livro excluido com sucesso!");
        }  

    }
}
