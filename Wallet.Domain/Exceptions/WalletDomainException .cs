using System;

namespace WalletService.Service.Domain.Exceptions
{
    public class WalletDomainException : Exception
    {
        public WalletDomainException()
        { }

        public WalletDomainException(string message)
            : base(message)
        { }

        public WalletDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
