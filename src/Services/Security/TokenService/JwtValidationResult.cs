namespace TokenService
{

    /// <summary>
    /// Used to generate standard validation messages when validating JWT's
    /// </summary>
    public class JwtValidationResult
    {
        public JwtValidationResult()
        { }

        /// <summary>
        /// Creates a new instance of <see cref="JwtValidationResult"/> with custom message
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="message"></param>
        public JwtValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public static JwtValidationResult SchemeNotSupported(string scheme)
            => new JwtValidationResult(false, $"The scheme {scheme} provided is not supported");

        public static JwtValidationResult Invalid
            => new JwtValidationResult(false, "The provided JWT token is not valid");
    }
}
