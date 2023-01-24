using orders.Api.Domain.Entites;
using orders.Api.Domain.valueObject;

namespace orders.Api.Application.Dto
{
    public class OrderRequest
    {
        public Customer? Customer { get; set; }
        public IList<Product>? Products { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal OrderTotal { get; set; }
        public ShippingType ShippingType
        {
            get; set;
        }
    }
}