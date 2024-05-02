using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Sanduba.Infrastructure.PaymentAPI.Configurations.Options
{
    public class PaymentConfigureOptions(IConfiguration configuration) : IConfigureOptions<PaymentOptions>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string _baseUrl = "PaymentSettings:BaseUrl";

        public void Configure(PaymentOptions options)
        {
            options.BaseUrl = _configuration.GetValue<string>(_baseUrl) ?? string.Empty;
        }
    }
}
