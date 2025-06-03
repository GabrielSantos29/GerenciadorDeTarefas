using ApiListaDeTerefas.Data;
using System;

namespace TesteConexaoConsole // Você pode ajustar o namespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando teste de conexão com o banco de dados...");

            // Crie uma instância da sua classe DataBase
            DataBase db = new DataBase();

            // Chame o método de teste (assumindo que ele se chama 'teste')
            db.Teste();

            Console.WriteLine("\nTeste de conexão finalizado. Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}