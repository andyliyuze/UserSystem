using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Usersystem.Web.Startup))]

namespace Usersystem.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Nancy
            app.UseNancy();
        }
    }
}
