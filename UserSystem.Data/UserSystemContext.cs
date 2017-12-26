using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UserSystem.Core.Entity;
namespace UserSystem.Data
{
    public class UserSystemContext: IdentityDbContext 
    {
        public UserSystemContext()
            : base("UserSystem")
        {
            //调试开发使用，每次都会先删除数据库再创建
            //   Database.SetInitializer<UserSystemContext>(new DropCreateDatabaseAlways<UserSystemContext>());
        }
    }
 
}
