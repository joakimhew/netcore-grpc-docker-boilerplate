namespace TokenService
{
    public class TokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public TokenOptions()
        {
            Issuer = "test";
            Audience = "test";
        }
    }
}
