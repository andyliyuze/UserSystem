using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Core.Entity
{
    public class User:IdentityUser
    {
        public DateTime BirthDay { get; set; }
    }
}
