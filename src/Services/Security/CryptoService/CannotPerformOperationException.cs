using System;

namespace CryptoService
{
    public sealed class CannotPerformOperationException : Exception
    {
        public CannotPerformOperationException()
        {
        }

        public CannotPerformOperationException(string message)
            :base(message)
        {
        }

        public CannotPerformOperationException(string message, Exception inner)
            :base(message, inner)
        {
        }
    }
}