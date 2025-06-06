using MySql.Data.MySqlClient;

namespace ApiListaDeTarefas.Data
{
    public class DataBase
    {
        private string ConnectionString = "Server=localhost;Port=3306;Database=DblistaTarefas;User=root;Password=admin;";

        public void Teste()
        {
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
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
                    Console.WriteLine("Erro: "+ex.ToString());
                }
            }
        }
    }
}
