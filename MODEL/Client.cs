using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MODEL
{
    public class Client
    {
        public string Name { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public string CellphoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime RegistrationDate { get; set; }
    }

}

