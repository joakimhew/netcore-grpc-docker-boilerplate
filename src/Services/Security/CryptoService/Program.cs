namespace CryptoService
{
    using System;
    using System.Threading;
    using Grpc.Core;

    public class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server 
            {
                Services = 
                { 
                    Security.API.CryptoService.BindService(
                        new CryptoServiceImpl(new Pbkdf2PasswordStorage())) 
                },
                
                Ports = { new ServerPort("0.0.0.0", 1115, ServerCredentials.Insecure) }
            };

            server.Start();
            Console.WriteLine("Crypto service listening at 0.0.0.0:1115");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
