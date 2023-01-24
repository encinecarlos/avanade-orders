namespace orders.Api.Domain.valueObject
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Shipped,
        Delivered,
        Cancelled,
        Refunded,
        Returned
    }
}