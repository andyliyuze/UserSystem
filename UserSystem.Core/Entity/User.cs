using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Core.Entity
{
    public class User:IdentityUser
    {
        public DateTime? Birthday { get; set; }

        public void UpdateInfo(string userName,DateTime? birthday,string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new Exception("用户名不可为空");
            }
            
            this.UserName = userName;
            this.Birthday = birthday;
            this.Email = email;
        }
    }
    
}
