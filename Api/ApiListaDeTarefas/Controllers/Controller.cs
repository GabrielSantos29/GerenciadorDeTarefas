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
        //método POST
        [HttpPost]
        public ActionResult<List<Tarefa>> Post([FromBody] Tarefa novaTarefa)
        {
            int id = _dataBase.AddTarefa(novaTarefa);
            novaTarefa.Id = id;
            //return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
            return Ok(novaTarefa);
        }
    }
}