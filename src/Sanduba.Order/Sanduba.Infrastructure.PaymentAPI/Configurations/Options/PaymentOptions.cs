using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanduba.Infrastructure.API.Payment.Configurations.Options
{
    public class PaymentOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string BrokerConnectionString { get; set; } = string.Empty;
        public string BrokerTopic { get; set; } = string.Empty;
    }
}
