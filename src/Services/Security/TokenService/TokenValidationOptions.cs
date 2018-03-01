using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace TokenService
{
    public class TokenValidationOptions
    {
        public TokenValidationParameters TokenValidationParameters { get; set; }
        public IList<string> SupportedSchemes { get; set; }

        public TokenValidationOptions()
        {
            var tokenSecrets = new TokenSecrets();
            var tokenOptions = new TokenOptions();

            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = tokenSecrets.SigningKey,

                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = tokenOptions.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromSeconds(30)
            };

            SupportedSchemes = new List<string>
            {
                "Bearer"
            };
        }
    }
}
