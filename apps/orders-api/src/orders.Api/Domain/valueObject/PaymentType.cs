using System.ComponentModel;

namespace orders.Api.Domain.valueObject
{
    public enum PaymentType
    {
        [Description("CreditCard")]
        CreditCard,
        [Description("DebitCard")]
        DebitCard,
        [Description("Paypal")]
        Paypal,
        [Description("GooglePay")]
        GooglePay,
        [Description("ApplePay")]
        ApplePay
    }
}