namespace Api.Data.Commands
{
    public class Install
    {
        public static string CreateTableCliente() => @"CREATE TABLE cliente(
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            nome VARCHAR(60) NOT NULL,
                            email VARCHAR(60) NOT NULL,
                            agencia INT,
                            conta INT,      
                            dac INT,
                            senha VARCHAR(20),
                            token varchar(255),
                            saldo FLOAT,
                            CONSTRAINT unique_email UNIQUE (email)
                            CONSTRAINT unique_agencia_conta_dac UNIQUE (agencia, conta,dac));";


        public static string CreateTableExtrato() => @"CREATE TABLE extrato(
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            id_cliente INT NOT NULL,
                            data_hora DATETIME NOT NULL,
                            literal VARCHAR(60),
                            valor FLOAT,
                            FOREIGN KEY (id_cliente) REFERENCES cliente(id));";
    }
}