namespace CryptoService
{
    using Grpc.Core;
    using Security.API;
    using static Security.API.CryptoService;
    using System;
    using System.Threading.Tasks;

    public class CryptoServiceImpl : CryptoServiceBase
    {
        private readonly IPasswordStorage passwordStorage;

        public CryptoServiceImpl(IPasswordStorage passwordStorage)
        {
            this.passwordStorage = passwordStorage;
        }

        public override Task<HashResponse> CreateHash(HashRequest hashRequest, ServerCallContext context) 
        {
            var hash = this.passwordStorage.CreateHash(hashRequest.Password);
            
            return Task.FromResult(new HashResponse {
                Hash = hash
            });
        }
        

        public override Task<VerifyHashResponse> VerifyHash(VerifyHashRequest verifyHashRequest, ServerCallContext context) 
        {
            var verify = this.passwordStorage.VerifyPassword(
                verifyHashRequest.Password,
                verifyHashRequest.Hash
            );

            return Task.FromResult(new VerifyHashResponse {
                IsValid = verify
            });
        }
    }
}