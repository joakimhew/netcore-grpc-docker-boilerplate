using Microsoft.IdentityModel.Tokens;
using System;

namespace TokenService
{
    public class TokenGenerationOptions
    {
        public TimeSpan Expiration { get; set; } 
        public SigningCredentials SigningCredentials { get; set; }

        public TokenGenerationOptions()
        {
            var tokenSecrets = new TokenSecrets();

            Expiration = TimeSpan.FromHours(4);
            SigningCredentials = new SigningCredentials(tokenSecrets.SigningKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
