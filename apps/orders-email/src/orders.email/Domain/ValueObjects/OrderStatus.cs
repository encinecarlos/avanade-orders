namespace orders.email.Domain.valueObject
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