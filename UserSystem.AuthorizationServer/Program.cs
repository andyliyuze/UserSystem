using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.AuthorizationServer
{
   internal class Program
    {
         static void Main(string[] args)
        {
            const string uri = "http://localhost:40048/";
            //
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started listening on " + uri);
                Console.ReadLine();
                Console.WriteLine("Shutting down...");
            }
            Console.ReadLine();
        }
    }
}
