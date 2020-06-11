using System;

namespace WalletService.API.Application.Exceptions
{
    public class WalletApplicationException : Exception
    {
        public WalletApplicationException()
        { }

        public WalletApplicationException(string message)
            : base(message)
        { }

        public WalletApplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
