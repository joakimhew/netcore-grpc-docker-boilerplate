namespace CryptoService
{
    using System;
    using System.Threading;
    using Grpc.Core;

    public class Program
    {
        static void Main(string[] args)
        {
            if(!int.TryParse(Environment.GetEnvironmentVariable("CRYPTO_SERVICE_PORT"), out int port))
                throw new FormatException("Could not parse the environment variable: 'CRYPTO_SERVICE_PORT'");

            Server server = new Server
            {
                Services =
                {
                    Security.API.CryptoService.BindService(
                        new CryptoServiceImpl(new Pbkdf2PasswordStorage())
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
