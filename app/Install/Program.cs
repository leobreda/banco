using Api.Data.Commands;
using Api.Models;
using Dapper;
using Microsoft.Data.Sqlite;

public class Program
{
    private static string DATABASE_FILE = "database.db";
    public static void Main(string[] args)
    {
        string database_file = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + "/" + DATABASE_FILE;

        if (System.IO.File.Exists(database_file))
        {
            File.Delete(database_file);
            //Environment.Exit(0);
        }

        Console.WriteLine("Instalando banco de dados...");

        using (var connection = new SqliteConnection($"Data Source={database_file}"))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                Console.WriteLine("criando tabela [cliente]...");
                command.CommandText = Install.CreateTableCliente();
                command.ExecuteNonQuery();
            }

            Console.WriteLine("criando conta 1000/20000-3...");
            Api.Models.Cliente cliente = new Api.Models.Cliente()
            {
                nome = "Fulano Beltrano",
                email = "fulano@funalo.com",
                agencia = 1000,
                conta = 20000,
                dac = 3,
                senha = "1000200003",
                token=""
            };
            
            connection.ExecuteScalar<int>(Api.Data.Commands.Cliente.Insert(), cliente);


            //using (var command = connection.CreateCommand())
            //{
            //    Console.WriteLine("criando tabela [extrato]...");
            //    command.CommandText = Install.CreateTableExtrato();
            //    command.ExecuteNonQuery();
            //}
            connection.Close();
        }

        Console.WriteLine("Banco de dados instalado com sucesso!");
        Console.WriteLine("Agora, Execute a api...");
    }
}