using Api.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.ContaCorrente
{
    public class DepositoResponse : ContaCorrente
    {

        public string nome { get; set; }

        public decimal valor { get; set; }

        public string token { get; set; }

        public DepositoResponse()
        {

            nome = string.Empty;
            token = string.Empty;
        }



    }
}
