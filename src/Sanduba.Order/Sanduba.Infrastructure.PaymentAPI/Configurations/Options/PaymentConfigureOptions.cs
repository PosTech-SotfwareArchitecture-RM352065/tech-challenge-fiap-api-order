﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Sanduba.Infrastructure.API.Payment.Configurations.Options
{
    public class PaymentConfigureOptions(IConfiguration configuration) : IConfigureOptions<PaymentOptions>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly string _baseUrl = "PaymentSettings:BaseUrl";
        private readonly string _brokerConnectioString = "PaymentSettings:BrokerConnectionString";
        private readonly string _brokerTopic = "PaymentSettings:TopicName";

        public void Configure(PaymentOptions options)
        {
            options.BaseUrl = _configuration.GetValue<string>(_baseUrl) ?? string.Empty;
            options.BrokerConnectionString = _configuration.GetValue<string>(_baseUrl) ?? string.Empty;
            options.BrokerTopic = _configuration.GetValue<string>(_baseUrl) ?? string.Empty;
        }
    }
}
