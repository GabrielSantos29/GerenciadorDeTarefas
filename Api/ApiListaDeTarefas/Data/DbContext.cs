using MySql.Data.MySqlClient;
using ApiListaDeTarefas.Models;

namespace ApiListaDeTarefas.Data
{
    public class DataBase
    {
        private string ConnectionString = "Server=localhost;Port=3306;Database=DblistaTarefas;User=root;Password=admin;";

        public void Teste()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Conectado com o bacnco de dados !");
                    conn.Close();
                    Console.WriteLine("Conexão encerrada");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.ToString());
                }
            }
        }
        // Método para buscar todas as tarefas obs: retorna uma lista com as tarefas.
        public List<Tarefa> GetTarefas()
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string Query = "SELECT Id, Nome, Concluido FROM Tarefas;";
                    using (MySqlCommand cmd = new MySqlCommand(Query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Tarefa tarefa = new Tarefa
                                {
                                    Id = reader.GetInt32("Id"),
                                    Nome = reader.GetString("Nome"),
                                    Concluida = reader.GetBoolean("Concluido")
                                };
                                tarefas.Add(tarefa);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.ToString());
                }
            }

            return tarefas;
        }
        // Método POST (inserir)
        public int AddTarefa(Tarefa tarefa)
        {
            int idGerado = 0;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string Query = "INSERT INTO Tarefas (Nome, Concluido) VALUES (@Nome,@Concluido);SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand(Query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", tarefa.Nome);
                        cmd.Parameters.AddWithValue("@Concluido", tarefa.Concluida);
                        idGerado = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.ToString());
                }
            } 
            return idGerado;
        }
    }
}
