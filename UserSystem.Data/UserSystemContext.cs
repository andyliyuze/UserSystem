using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using UserSystem.Core.Entity;
using UserSystem.Data.Configurations;

namespace UserSystem.Data
{
    public class UserSystemContext: IdentityDbContext<User>
    {
        public virtual IDbSet<AppClient> AppClient { get; set; }

        public UserSystemContext()
            : base("UserSystem")
        {
            //调试开发使用，每次都会先删除数据库再创建
            //   Database.SetInitializer<UserSystemContext>(new DropCreateDatabaseAlways<UserSystemContext>());
        }

        //实体配置
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AppClientConfiguration());
        }

    }


    public class CustomUserManager : UserManager<User> 
    {
        public CustomUserManager(IUserStore<User> store):base(store)
        {

        }
    }

    public class CustomUserStore : UserStore<User>
    {
        public CustomUserStore(DbContext context):base(context)
        {

        }
    }
}
