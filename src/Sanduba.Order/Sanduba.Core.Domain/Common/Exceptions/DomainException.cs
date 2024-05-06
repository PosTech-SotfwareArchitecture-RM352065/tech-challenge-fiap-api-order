using System;

namespace Sanduba.Core.Domain.Common.Exceptions
{
    public class DomainException : Exception
    {
        private DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
