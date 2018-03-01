using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TokenService
{
    internal class TokenSecrets
    {
        internal SymmetricSecurityKey SigningKey { get; set; } 

        internal TokenSecrets()
        {
            var secret = "supersecret12356789!";

            SigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(secret));
        }
    }
}
