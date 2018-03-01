using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TokenService
{
    /// <summary>
    /// Used for validating provided JWT's against token validation options found in <see cref="Security.Token.TokenValidationOptions"/>
    /// </summary>
    public class JwtValidation
    {

        private readonly TokenValidationOptions _tokenValidationOptions;

        public JwtValidation()
        {
            _tokenValidationOptions = new TokenValidationOptions();
        }

        /// <summary>
        /// Validates a JWT by providing the encoded version (access token) and schema. Sets the validation parameters to default.
        /// </summary>
        /// <param name="accessToken">The encoded JWT</param>
        /// <param name="schema">The schema of which the JWT falls under</param>
        /// <returns></returns>
        public JwtValidationResult Validate(string accessToken, string schema)
        {
            return Validate(accessToken, _tokenValidationOptions.TokenValidationParameters, schema);
        }

        /// <summary>
        /// Validates a JWT by providing the encoded version (access token), schema and token validation parameters. 
        /// </summary>
        /// <param name="accessToken">The encoded JWT</param>
        /// <param name="tokenValidationParameters">The validation parameters to use when validating the JWT</param>
        /// <param name="schema">The schema of which the JWT falls under</param>
        /// <returns></returns>
        private JwtValidationResult Validate(string accessToken, TokenValidationParameters tokenValidationParameters,
            string schema = "Bearer")
        {
            if (!_tokenValidationOptions.SupportedSchemes.Contains(schema))
            {
                return JwtValidationResult.SchemeNotSupported(schema);
            }

            return string.IsNullOrWhiteSpace(accessToken) ?
                JwtValidationResult.Invalid :
                ValidateAccessToken(accessToken, tokenValidationParameters);
        }

        /// <summary>
        /// Validates the accessToken part of the JWT https://bshaffer.github.io/oauth2-server-php-docs/overview/jwt-access-tokens/
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="tokenValidationParameters"></param>
        /// <returns></returns>
        private JwtValidationResult ValidateAccessToken(string accessToken, TokenValidationParameters tokenValidationParameters)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            if (!jwtSecurityTokenHandler.CanValidateToken)
            {
                return new JwtValidationResult(
                    isValid: false,
                    message: "The JWT was corrupted and could not be read/validated");
            }

            SecurityToken validatedToken;

            try
            {
                var claims = jwtSecurityTokenHandler.ValidateToken(accessToken, tokenValidationParameters, out validatedToken);

                if (!claims.Identity.IsAuthenticated)
                {
                    return JwtValidationResult.Invalid;
                }

                return new JwtValidationResult(
                    isValid: true,
                    message: "The provided JWT token is valid");
            }

            catch
            {
                return JwtValidationResult.Invalid;
            }
        }
    }
}
