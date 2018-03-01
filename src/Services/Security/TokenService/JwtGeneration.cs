using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Security.API;

namespace TokenService
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtGeneration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public Jwt Generate(List<Claim> claims)
        {
            if (claims == null || !claims.Any())
            {
                throw new ArgumentNullException(nameof(claims));
            }

            var now = DateTime.UtcNow;
            var timespan = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1));
            var unixTimestamp = (int)timespan.TotalSeconds;

            var tokenGenerationOptions = new TokenGenerationOptions();
            var tokenOptions = new TokenOptions();

            var jwtHeader = new JwtHeader(tokenGenerationOptions.SigningCredentials);
            var payload = new JwtPayload(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(tokenGenerationOptions.Expiration));

            payload.AddClaim(new Claim("jti", Guid.NewGuid().ToString()));
            payload.AddClaim(new Claim("iat", unixTimestamp.ToString()));

            var jwt = new JwtSecurityToken(jwtHeader, payload);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var jwtDto = new Jwt
            {
                AccessToken = encodedJwt,
                Schema = "Bearer"
            };

            return jwtDto;
        }
    }
}
