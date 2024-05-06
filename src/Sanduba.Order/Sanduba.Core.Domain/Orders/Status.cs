namespace Sanduba.Core.Domain.Orders
{
    public enum Status
    {
        Created,
        WaitingPayment,
        Payed,
        Accepted,
        Reject,
        Ready,
        Concluded,
        Cancelled
    }
}
