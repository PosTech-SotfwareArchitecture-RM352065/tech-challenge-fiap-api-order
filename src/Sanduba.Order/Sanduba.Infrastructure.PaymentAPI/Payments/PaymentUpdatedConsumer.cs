using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sanduba.Infrastructure.API.Payment.Payments
{
    public record PaymentUpdated(string Status, string? PaymentId, DateTimeOffset? PayedAt);
    public class PaymentUpdatedConsumer(
        ILogger<PaymentUpdatedConsumer> logger,
        IPublishEndpoint publishClient
    ) : IConsumer<PaymentUpdated>
    {
        private readonly ILogger<PaymentUpdatedConsumer> _logger = logger;

        public async Task Consume(ConsumeContext<PaymentUpdated> context)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                await context.NotifyConsumed(timer.Elapsed, nameof(PaymentUpdatedConsumer));
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(), ex, "Error consuming payment update");
                await context.NotifyFaulted(timer.Elapsed, nameof(PaymentUpdatedConsumer), ex);
            }
            finally
            {
                timer.Stop();
            }
            throw new NotImplementedException();

        }
    }
}
