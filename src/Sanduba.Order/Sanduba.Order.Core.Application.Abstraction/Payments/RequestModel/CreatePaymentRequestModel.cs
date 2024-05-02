using Sanduba.Core.Domain.Orders;
using Sanduba.Core.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Payments.RequestModel
{
    public record CreatePaymentRequestModel(Order Order, Method Method, Provider Provider);
}
