using Api.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Api.Data
{
    public interface IContaCorrenteData
    {
        Task<decimal> Saldo(Cliente cliente);

        Task<bool> Depositar(Cliente cliente, decimal valor);
        Task<bool> SimularSaque(Cliente cliente, decimal valor);
        Task<bool> Sacar(Cliente cliente, decimal valor);

    }
    public class ContaCorrenteData : IContaCorrenteData
    {
        private readonly AppSettings appSettings;

        public ContaCorrenteData(AppSettings _appSettings)
        {
            appSettings = _appSettings;
        }

        public async Task<decimal> Saldo(Cliente cliente)
        {
            decimal ret = 0;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.ExecuteScalar<decimal>(Commands.ContaCorrente.Saldo(cliente.id, cliente.senha));
                connection.Close();
            }

            return ret;
        }

        public async Task<bool> Depositar(Cliente cliente, decimal valor)
        {
            bool ret = false;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.ExecuteScalar<int>(Commands.ContaCorrente.Deposito(cliente.id, valor)) > 0 ? true : false;
                connection.Close();
            }

            return ret;
        }
        public async Task<bool> SimularSaque(Cliente cliente, decimal valor)
        {
            bool ret = false;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.ExecuteScalar<decimal>(Commands.ContaCorrente.SimularSaque(cliente.id, valor)) >= (decimal)0 ? true:false;
                connection.Close();
            }

            return ret;
        }

        public async Task<bool> Sacar(Cliente cliente, decimal valor)
        {
            bool ret = false;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.Execute(Commands.ContaCorrente.Saque(cliente.id, valor)) > 0 ? true : false;
                connection.Close();
            }

            return ret;
        }
    }
}
