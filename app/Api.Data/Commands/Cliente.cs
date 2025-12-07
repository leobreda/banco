namespace Api.Data.Commands
{
    public class Cliente
    {
        public static string Insert() => @"INSERT INTO cliente (nome, email, agencia, conta, dac, senha, token, saldo) 
                                            VALUES (@nome, @email, @agencia, @conta, @dac, @senha, @token, @saldo);
                                            SELECT last_insert_rowid() AS id";

        public static string Select() => "select id, nome, email, agencia, conta, dac from cliente";

        public static string Update() => "update cliente set nome=@nome, email=@email where id=@id";

        public static string Delete() => "delete from cliente where \"id\"=@id";
    }
}