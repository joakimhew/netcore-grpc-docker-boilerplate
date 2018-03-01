namespace TokenService
{
    using Grpc.Core;
    using Security.API;
    using System;
    using static Security.API.TokenService;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Security.Claims;
    using System.Collections.Generic;

    public class TokenServiceImpl : TokenServiceBase
    {
        private readonly JwtValidation jwtValidation;
        private readonly JwtGeneration jwtGeneration;

        public TokenServiceImpl(
            JwtValidation jwtValidation,
            JwtGeneration jwtGeneration)
        {
            this.jwtValidation = jwtValidation ?? throw new ArgumentNullException(nameof(jwtValidation));
            this.jwtGeneration = jwtGeneration ?? throw new ArgumentNullException(nameof(jwtGeneration));
        }
        public override async Task<TokenValidationResponse> ValidateToken(
            TokenValidationRequest request, 
            ServerCallContext context) 
        {
            var validationResult = this.jwtValidation.Validate(
                request.Jwt.AccessToken,
                request.Jwt.Schema);

            return await Task.FromResult(new TokenValidationResponse
            {
                IsValid = validationResult.IsValid
            });
        }

        public override async Task<TokenGenerationResponse> GenerateToken(
            TokenGenerationRequest request, 
            ServerCallContext context) 
        {
            var claims = new List<Claim>();

            foreach(var claim in request.Claims)
            {
                claims.Add(new Claim(claim.Name, claim.Value));
            }

            var jwt = this.jwtGeneration.Generate(
                claims
            );

            return await Task.FromResult(new TokenGenerationResponse 
            {
                Jwt = jwt
            });
        }
    }
}