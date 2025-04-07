using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class DocProcess
    {
        public string Type { get; set; } = null!;

        public string ProcessNumber { get; set; } = null!;

        public string EstablishmentFantasyName { get; set; } = null!;

        public string ClientName { get; set; } = null!;

        public DateTime IssuanceDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Status { get; set; } = null!;

        public string Extra { get; set; } = null!;
    }
}
