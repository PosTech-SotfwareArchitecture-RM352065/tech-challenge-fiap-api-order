using Sanduba.Core.Domain.Common.Types;
using System;
using System.Collections.Generic;

namespace Sanduba.Core.Domain.Payments
{
    public sealed class Payment : ValueObject
    {
        public Guid Id { get; set; }
        public Method Method { get; set; }
        public Provider Provider { get; set; }
        public string Status { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
