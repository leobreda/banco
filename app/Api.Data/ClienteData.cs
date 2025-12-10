using Api.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Api.Data
{
    public interface IClienteData
    {
        Task<int> Create(Cliente cliente);
        Task<Cliente> Login(Cliente cliente);
        Task<bool> UpdateToken(Cliente cliente, string token);
        Task<List<Cliente>> ListAll();
        Task<List<Cliente>> Search(Cliente cliente);

        Task<int> Update(Cliente cliente);
        Task<int> Delete(Cliente cliente);
    }
    public class ClienteData : IClienteData
    {
        private readonly AppSettings appSettings;

        public ClienteData(AppSettings _appSettings)
        {
            appSettings = _appSettings;
        }

        public async Task<int> Create(Cliente cliente)
        {
            int ret = 0;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.ExecuteScalar<int>(Commands.Cliente.Insert(), cliente);
                connection.Close();
            }

            return ret;
        }

        public async Task<Cliente> Login(Cliente cliente)
        {
           return (await Select(Commands.Cliente.Login(), cliente)).FirstOrDefault<Cliente>();
        }

        public async Task<bool> UpdateToken(Cliente cliente, string token)
        {
            int ret = 0;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.Execute(Commands.Cliente.UpdateToken(cliente.id, token));
                connection.Close();
            }

            if (ret.Equals(0))
                return false;

            return true;
        }


        public async Task<List<Cliente>> ListAll()
        {
            return await Select(Commands.Cliente.Select());
        }

        public async Task<List<Cliente>> Search(Cliente cliente)
        {
            List<Cliente> ret = new List<Cliente>();

            string str = Commands.Cliente.Select() + " where";

            string where = "";

            if (!cliente.id.Equals(0))
                where += " and id=@id";

            if (!string.IsNullOrEmpty(cliente.nome))
                where += " and lower(nome)=lower(@nome)";

            if (!string.IsNullOrEmpty(cliente.email))
                where += " and lower(email)=lower(@email)";

            if (!cliente.agencia.Equals(0))
                where += " and agencia=@agencia";

            if (!cliente.conta.Equals(0))
                where += " and conta=@conta";

            if (!cliente.dac.Equals(0))
                where += " and dac=@dac";

            if (!string.IsNullOrEmpty(cliente.senha))
                where += " and lower(senha)=lower(@senha)";

            string query = str + (where.StartsWith(" and") ? where.Substring(4) : where);

            return await Select(query,cliente);

        }

        public async Task<int> Update(Cliente cliente)
        {
            int ret = 0;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.Execute(Commands.Cliente.Update(), cliente);
                connection.Close();
            }
            return ret;
        }

        public async Task<int> Delete(Cliente cliente)
        {
            int ret = 0;

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = connection.Execute(Commands.Cliente.Delete(), cliente);
                connection.Close();
            }
            return ret;
        }
        private async Task<List<Cliente>> Select(string query, Cliente cliente = null)
        {
            List<Cliente> ret = new List<Cliente>();

            using (var connection = new SqliteConnection(appSettings.ConnectionString))
            {
                connection.Open();
                ret = (List<Cliente>) await connection.QueryAsync<Cliente>(query,cliente);
                connection.Close();
            }
            return ret;
        }
    }
}