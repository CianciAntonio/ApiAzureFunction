using Dtos.CustomerResponse;
using Dtos.ModelRequest;
using EntityFrameworkClassLibrary.Models;
using Mapster;
using System.Threading.Tasks;

namespace ApiFunctionWithRepositoryPattern
{
    public class ModelMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {        
            config.ForType<Customer, CustomerResponse>()
                .Map(dest => dest.FullName, src => $"{src.SurName} {src.LastName}")
                .Map(dest => dest.Invoices, src => src.Invoices);

            config.ForType<Invoice, InvoiceResponse>()
                .Map(dest => dest.Order, src => $"{src.Quantity} product/s costing {src.Price}€ on {src.OrderDate}")
                .Map(dest => dest.ProductDescription, src => src.Product)
                .Map(dest => dest.IdCustomer, src => src.CustomerId)
                .Map(dest => dest.Customer, src => $"{src.Customer.SurName} {src.Customer.LastName}");

            config.ForType<Product, ProductResponse>()
                .Map(dest => dest.Product, src => $"{src.ProductName}, {src.ProductDescription} in category {src.ProductCategory}")
                .Map(dest => dest.IdInvoice, src => src.InvoiceId);
            
            config.ForType<CustomerRequest, Customer>();
            config.ForType<InvoiceRequest, Invoice>();
            config.ForType<ProductRequest, Product>();
        }
    }
}
