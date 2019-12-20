using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var webHost = CreateWebHostBuilder(args).Build();

                using (var scope = webHost.Services.CreateScope())
                {
                    var myDbContext = scope.ServiceProvider.GetRequiredService<GODDataContext>();

                    //Do the migration asynchronously
                    myDbContext.Database.EnsureCreated();
                }
                webHost.Run();
            }
            finally
            {
                Console.WriteLine("CLose");
            }
        }

         public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
