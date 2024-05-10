using AutoMapper;
using Microsoft.Extensions.Options;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Core.Application.Abstraction.Payments.RequestModel;
using Sanduba.Core.Application.Abstraction.Payments.ResponseModel;
using Sanduba.Infrastructure.PaymentAPI.Configurations.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using GatewayPaymentRequest = Sanduba.Infrastructure.PaymentAPI.Payments.RequestModel.CreatePaymentRequestModel;
using GatewayPaymentResponse = Sanduba.Infrastructure.PaymentAPI.Payments.ResponseModel.CreatePaymentResponseModel;

namespace Sanduba.Infrastructure.PaymentAPI.Payments
{
    public class PaymentGateway(IOptions<PaymentOptions> options, IMapper mapper) : IPaymentGateway
    {
        private readonly PaymentOptions _options = options.Value;
        private readonly IMapper _mapper = mapper;

        public async Task<CreatePaymentResponseModel> CreatePayment(CreatePaymentRequestModel requestModel)
        {
            var paymentUrl = $"{_options.BaseUrl}/PaymentCreation";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, paymentUrl);

            var gatewayRequest = _mapper.Map<GatewayPaymentRequest>(requestModel);
            var content = new StringContent(JsonSerializer.Serialize(gatewayRequest, jsonOptionsSnakeCase));
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var reader = response.Content.ReadAsStringAsync();
            reader.Wait();

            var gatewayResponse = JsonSerializer.Deserialize<GatewayPaymentResponse>(reader.Result, jsonOptionsCamelCase);

            return _mapper.Map<CreatePaymentResponseModel>(gatewayResponse);
        }

        private JsonSerializerOptions jsonOptionsSnakeCase = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            },
            WriteIndented = true,
        };

        private JsonSerializerOptions jsonOptionsCamelCase = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            },
            WriteIndented = true,
        };

    }
}
