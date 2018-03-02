using System;
using System.Threading;
using Grpc.Core;

namespace TokenService
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!int.TryParse(Environment.GetEnvironmentVariable("TOKEN_SERVICE_PORT"), out int port))
                throw new FormatException("Could not parse the environment variable: 'TOKEN_SERVICE_PORT'");

            Server server = new Server
            {
                Services =
                {
                    Security.API.TokenService.BindService(
                        new TokenServiceImpl(
                            new JwtValidation(),
                            new JwtGeneration()
                        )
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
            Console.WriteLine($"Token service listening at 0.0.0.0:{port}");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
