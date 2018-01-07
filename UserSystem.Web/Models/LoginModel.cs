using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserSystem.Web.Models
{
    public class LoginModel
    {
        public string userName { get; set; }

        public string password { get; set; }

        public string client_Id { get; set; }

        public string grant_type { get; set; }
    }
}