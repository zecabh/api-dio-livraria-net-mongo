using livrariaDIOAPI.Data.Collections;
using livrariaDIOAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace livrariaDIOAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditoraController : ControllerBase
    {

        Data.MongoDB _mongoDB;
        IMongoCollection<Editora> _editoraCollection;

        public EditoraController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _editoraCollection = _mongoDB.DB.GetCollection<Editora>(typeof(Editora).Name.ToLower());
        }

        // POST api/<EditoraController>
        [HttpPost]
        public async Task<IActionResult> InserirEditora([FromBody] EditoraDTO e)
        {
            var editora = new Editora(e._IdEditora, e.NomeEditora);

            await _editoraCollection.InsertOneAsync(editora);

            return StatusCode(201, "Editora adicionada com sucesso!");
        }


        // GET: api/<EditoraController>
        [HttpGet]
        public async Task<IActionResult> GetEditoras()
        {
            var editoras = await _editoraCollection.Find(Builders<Editora>.Filter.Empty).ToListAsync();

            return Ok(editoras);
        }


        // GET api/<EditoraController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Editora>> GetEditoraPorId(string id)
        {
            var filter = Builders<Editora>.Filter.Eq(x => x._IdEditora, id);

            var editora = await _editoraCollection.Find(filter).FirstOrDefaultAsync();

            if (editora == null)
            {
                return NotFound();
            }

            return editora;
        }

        // PUT api/<EditoraController>/5
       [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateEditora(string id, [FromBody] EditoraDTO edit)
        {
            var filter = Builders<Editora>.Filter.Eq(x => x._IdEditora, id);

            var editora = await _editoraCollection.Find(filter).FirstOrDefaultAsync();

            if (editora == null)
            {
                return NotFound();
            }

            var updateEditora = new Editora(editora._IdEditora, edit.NomeEditora);

            await _editoraCollection.ReplaceOneAsync(filter, updateEditora);


            return StatusCode(200, "Editora alterada com sucesso!");
        }

        // DELETE api/<EditoraController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteEditora(string id)
        {
            var filter = Builders<Editora>.Filter.Eq(x => x._IdEditora, id);

            var editora = await _editoraCollection.Find(filter).FirstOrDefaultAsync();

            if (editora == null)
            {
                return NotFound();
            }

            await _editoraCollection.DeleteOneAsync(filter);

            return StatusCode(200, "Editora excluida com sucesso!");
        }

    }
}
