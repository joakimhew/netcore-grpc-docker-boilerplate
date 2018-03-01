using System;
using System.Threading;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Security.Domain;

namespace UserService
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var options = new DbContextOptionsBuilder<SecurityDbContext>()
                .UseInMemoryDatabase("Users")
                .Options;

            var securityDbContext = new SecurityDbContext(options);

            Server server = new Server
            {
                Services =
                {
                    Security.API.UserService.BindService(
                        new UserServiceImpl(options)
                    )
                },

                Ports = { new ServerPort("0.0.0.0", 1113, ServerCredentials.Insecure) }
            };

            server.Start();
            Console.WriteLine("User service listening at 0.0.0.0:1113");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
