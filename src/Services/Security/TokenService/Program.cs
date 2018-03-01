using System;
using System.Threading;
using Grpc.Core;

namespace TokenService
{
    class Program
    {
        static void Main(string[] args)
        {
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
                
                Ports = { new ServerPort("0.0.0.0", 1114, ServerCredentials.Insecure) }
            };

            server.Start();
            Console.WriteLine("Token service listening at 0.0.0.0:1114");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
