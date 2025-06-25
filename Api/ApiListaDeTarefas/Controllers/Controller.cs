using Microsoft.AspNetCore.Mvc;
using ApiListaDeTarefas.Data;
using ApiListaDeTarefas.Models;

namespace ApiListaDeTarefas.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TarefasController : ControllerBase
    {
        //Injetando Banco de dados
        private readonly DataBase _dataBase;

        public TarefasController(DataBase dataBase)
        {
            _dataBase = dataBase;
        }
        //método GET 
        [HttpGet]
        public ActionResult<List<Tarefa>> Get()
        {
            var tarefas = _dataBase.GetTarefas();
            return Ok(tarefas);
        }
        //método Get(id)
        [HttpGet("{id}")]
        public ActionResult<Tarefa> GetTarefaId(int id)
        {
            var tarefa = _dataBase.GetTarefaId(id);
            if (tarefa == null)
            {
                return NotFound();// 404 se não existir
            }
            return Ok(tarefa);// 200 com o objeto
        }
        //método POST
        [HttpPost]
        public ActionResult<List<Tarefa>> Post([FromBody] Tarefa novaTarefa)
        {
            int id = _dataBase.AddTarefa(novaTarefa);
            novaTarefa.Id = id;
            //return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
            return Ok(novaTarefa);
        }
        //método PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tarefa tarefaAtualizada)
        {
            if (id != tarefaAtualizada.Id)
                return BadRequest("ID da URL diferente do ID do corpo.");
            bool sucesso = _dataBase.AtualizarTarefa(tarefaAtualizada);
            return sucesso ? NoContent() : NotFound();

        }
        //método Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool Excluiu = _dataBase.DeletarTarefa(id);
            if (Excluiu)
            {
                return NoContent();// 204 – excluído com sucesso
            }
            else
            {
                return NotFound();//404 – não encontrou para deletar
            }
        }
    }
}