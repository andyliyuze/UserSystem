using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSystem.Core.Entity;
using System.Data.Entity;

namespace UserSystem.Data
{
    public class UserSystemContext: IdentityDbContext<User>,IDbContext
    {
        public UserSystemContext()
            : base("UserSystem")
        {
            //调试开发使用，每次都会先删除数据库再创建
            //   Database.SetInitializer<UserSystemContext>(new DropCreateDatabaseAlways<UserSystemContext>());

        }
        

    }
}
