using Grpc.Core;

namespace Security.API
{
    public static class Communication
    {
        public static Channel CryptoChannel = new Channel(
            "cryptoservice:1115", 
            ChannelCredentials.Insecure); 
        
        public static Channel TokenChannel = new Channel(
            "tokenservice:1114", 
            ChannelCredentials.Insecure);

        public static Channel UserChannel = new Channel(
            "userservice:1113",
            ChannelCredentials.Insecure);
    }
}