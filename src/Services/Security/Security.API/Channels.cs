using System;
using Grpc.Core;

namespace Security.API
{
    public static class Channels
    {

        private static string CryptoServiceName = 
            Environment.GetEnvironmentVariable("CRYPTO_SERVICE_NAME");

        private static int CryptoServicePort = 
            int.Parse(Environment.GetEnvironmentVariable("CRYPTO_SERVICE_PORT"));

        private static string TokenServiceName = 
            Environment.GetEnvironmentVariable("TOKEN_SERVICE_NAME");

        private static int TokenServicePort = 
            int.Parse(Environment.GetEnvironmentVariable("TOKEN_SERVICE_PORT"));

        
        private static string UserServiceName = 
            Environment.GetEnvironmentVariable("USER_SERVICE_NAME");

        private static int UserServicePort = 
            int.Parse(Environment.GetEnvironmentVariable("USER_SERVICE_PORT"));


        public static Channel CryptoChannel = new Channel(
            $"{CryptoServiceName}:{CryptoServicePort}", 
            ChannelCredentials.Insecure); 
        
        public static Channel TokenChannel = new Channel(
            $"{TokenServiceName}:{TokenServicePort}", 
            ChannelCredentials.Insecure);

        public static Channel UserChannel = new Channel(
            $"{UserServiceName}:{UserServicePort}",
            ChannelCredentials.Insecure);
    }
}