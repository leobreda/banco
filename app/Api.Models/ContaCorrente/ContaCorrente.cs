using Api.Bootstrap;

namespace Api.Models.ContaCorrente
{
    public abstract class ContaCorrente
    {
        public uint agencia { get; set; }
        public uint conta { get; set; }
        public uint dac { get; set; }
        public ContaCorrente()
        {
            agencia = 0;
            conta = 0;
            dac = 0;
        }

        public (bool valid, string message) IsValid()
        {
            string invalidFields = "";

            if (agencia.Equals(0) || agencia > 9999)
                invalidFields += $"Agencia {agencia} invalida. ";

            if (conta.Equals(0) || conta > 99999)
                invalidFields += $"Conta {conta} invalida. ";

            if (dac > 9)
                invalidFields += $"DAC {conta} invalido. ";

            if (string.IsNullOrEmpty(invalidFields))
                return (true, "");

            invalidFields = invalidFields.Trim();

            return (false, invalidFields);
        }

        public (bool valid, string message) IsValid(string token, uint token_expires)
        {
            var isvalid = IsValid();

            if (!Token.IsValid(token, agencia, conta, dac, token_expires))
                isvalid.message += "Token invalido.";

            if (string.IsNullOrEmpty(isvalid.message))
                return (true, "");

            return (false, isvalid.message);
        }
        public (bool valid, string message) IsValid(decimal valor)
        {
            var isvalid = IsValid();

            if (valor.Equals(0))
                isvalid.message += "Valor invalido.";

            if (string.IsNullOrEmpty(isvalid.message))
                return (true, "");

            return (false, isvalid.message);
        }
    }
}
