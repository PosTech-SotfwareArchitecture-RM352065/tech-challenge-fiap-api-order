using AutoMapper;
using Microsoft.Extensions.Options;
using Sanduba.Core.Application.Abstraction.Payments;
using Sanduba.Infrastructure.API.Payment.Configurations.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using CreatePaymentRequestModel = Sanduba.Core.Application.Abstraction.Payments.RequestModel.CreatePaymentRequestModel;
using CreatePaymentResponseModel = Sanduba.Core.Application.Abstraction.Payments.ResponseModel.CreatePaymentResponseModel;
using GatewayPaymentRequest = Sanduba.Infrastructure.API.Payment.Payments.RequestModel.CreatePaymentRequestModel;
using GatewayPaymentResponse = Sanduba.Infrastructure.API.Payment.Payments.ResponseModel.CreatePaymentResponseModel;

namespace Sanduba.Infrastructure.API.Payment.Payments
{
    public class PaymentGateway(IOptions<PaymentOptions> options, IMapper mapper) : IPaymentGateway
    {
        private readonly PaymentOptions _options = options.Value;
        private readonly IMapper _mapper = mapper;

        public async Task<CreatePaymentResponseModel> CreatePayment(CreatePaymentRequestModel requestModel, CancellationToken cancellation)
        {
            var paymentUrl = $"{_options.BaseUrl}/PaymentCreation";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, paymentUrl);

            var gatewayRequest = _mapper.Map<GatewayPaymentRequest>(requestModel);
            var content = new StringContent(JsonSerializer.Serialize(gatewayRequest, jsonOptionsSnakeCase));
            request.Content = content;

            var response = await client.SendAsync(request, cancellation);
            response.EnsureSuccessStatusCode();

            var reader = response.Content.ReadAsStringAsync(cancellation);
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
