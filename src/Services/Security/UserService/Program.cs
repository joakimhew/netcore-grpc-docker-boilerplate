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

            if(!int.TryParse(Environment.GetEnvironmentVariable("USER_SERVICE_PORT"), out int port))
                throw new FormatException("Could not parse the environment variable: 'USER_SERVICE_PORT'");

            Server server = new Server
            {
                Services =
                {
                    Security.API.UserService.BindService(
                        new UserServiceImpl(options)
                    )
                },

                Ports =
                {
                    new ServerPort(
                        "0.0.0.0", 
                        port, 
                        ServerCredentials.Insecure) 
                }
            };

            server.Start();
            Console.WriteLine($"User service listening at 0.0.0.0:{port}");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
