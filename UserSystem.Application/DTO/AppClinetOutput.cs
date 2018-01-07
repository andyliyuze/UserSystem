using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Application.DTO
{
    public class AppClinetOutput
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RetrunUrl { get; set; }

        public string ClientType { get; set; }
    }
}
