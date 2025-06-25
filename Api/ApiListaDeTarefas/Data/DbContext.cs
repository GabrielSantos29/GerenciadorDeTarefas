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
        //Método para buscar tarefas por ID
        public Tarefa? GetTarefaId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Id, Nome, Concluido FROM Tarefas WHERE Id = @Id LIMIT 1;";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Tarefa
                                {
                                    Id = reader.GetInt32("Id"),
                                    Nome = reader.GetString("Nome"),
                                    Concluida = reader.GetBoolean("Concluido")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.ToString());
                }
            }
            return null;
        }
        //Método POST (inserir)
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
        //Método PUT
        public bool AtualizarTarefa(Tarefa tarefa)
        {
            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                const string sql = @"
                UPDATE Tarefas
                    SET Nome = @Nome,
                        Concluido = @Concluido
                WHERE Id = @Id;
                ";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", tarefa.Id);
                cmd.Parameters.AddWithValue("@Nome", tarefa.Nome);
                cmd.Parameters.AddWithValue("@Concluido", tarefa.Concluida);

                // Executa o UPDATE e retorna quantas linhas foram afetadas
                int linhasAfetadas = cmd.ExecuteNonQuery();
                // Se afetou pelo menos uma linha, a atualização foi bem-sucedida
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
                return false;
            }
        }
        //Método DELETE
        public bool DeletarTarefa(int id)
        {
            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                const string sql = @"
                    DELETE FROM Tarefas
                        WHERE Id = @Id;
                ";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                // Executa o DELETE e retorna quantas linhas foram afetadas
                int linhasAfetadas = cmd.ExecuteNonQuery();
                // Se afetou pelo menos uma linha, a atualização foi bem-sucedida
                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
                return false;
            }
        }
    }
}
