using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MODEL
{
    public class Establishment
    {
        public string SocialReason { get; set; } = null!;

        public string FantasyName { get; set; } = null!;

        public string Cnpj { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string ProcessNumber { get; set; } = null!;

        public string ClientName { get; set; } = null!;
    }
}
