namespace Api.Data.Commands
{
    public class ContaCorrente
    {
        public static string Saldo(uint id, string senha) => $@"select saldo from cliente where id={id} and senha='{senha}';";

        public static string Deposito(uint id, decimal valor) => $@"update cliente set saldo = saldo +{valor.ToString().Replace(",", ".")} where id={id};
            SELECT changes() AS affectedRows";

        public static string SimularSaque(uint id, decimal valor) => $@"select saldo - {valor.ToString().Replace(",", ".")} as valor from cliente where id={id}";
        public static string Saque(uint id, decimal valor) => $@"update cliente set saldo = (saldo - {valor.ToString().Replace(",", ".")}) where id={id} and (saldo - {valor.ToString().Replace(",", ".")})>=0";
    }
}