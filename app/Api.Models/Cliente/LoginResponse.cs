using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class LoginResponse
    {
        public uint id { get; set;  }
        public string nome { get; set; }
        public string email { get; set; }
        public uint agencia { get; set; }
        public uint conta { get; set; }
        public uint dac { get; set; }
        public string token { get; set;  }

        public LoginResponse()
        {
            agencia = 0;
            conta = 0;
            dac = 0;
            token = string.Empty;
        }
    }
}
