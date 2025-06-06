namespace ApiListaDeTarefas.Models
{
    public class Tarefas
    {
        int Id { get; set; }
        string Nome { get; set; }
        bool Concluida { get; set; } = false;
    }
}
