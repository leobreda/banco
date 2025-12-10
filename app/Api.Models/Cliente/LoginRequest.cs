using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class LoginRequest
    {
        public uint agencia { get; set; }
        public uint conta { get; set; }
        public uint dac { get; set; }
        public string senha { get; set;  }

        public LoginRequest()
        {
            agencia = 0;
            conta = 0;
            dac = 0;
            senha = string.Empty;
        }
    }
}
