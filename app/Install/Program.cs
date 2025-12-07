using Api.Data.Commands;
using Microsoft.Data.Sqlite;

public class Program
{
    private static string DATABASE_FILE = "database.db";
    public static void Main(string[] args)
    {
        string database_file = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + "/" + DATABASE_FILE;

        if (System.IO.File.Exists(database_file))
            Environment.Exit(0);

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

            using (var command = connection.CreateCommand())
            {
                Console.WriteLine("criando tabela [extrato]...");
                command.CommandText = Install.CreateTableExtrato();
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        Console.WriteLine("Banco de dados instalado com sucesso!");
        Console.WriteLine("Agora, Execute a api...");
    }
}