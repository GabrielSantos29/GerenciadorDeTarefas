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
        //
    }
}