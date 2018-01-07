using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Core.Entity
{
    public class AppClient
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RetrunUrl { get; set; }

        public string ClientType { get; set; }
    }
}
