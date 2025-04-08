namespace imsapi.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imsapi.Data;
using imsapi.DTO;
using imsapi.DTO.Payment;
using IMSAPI.DTO.Products;
using Microsoft.EntityFrameworkCore;

public class PaymentService : IPaymentService
{

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }
    private readonly AppDbContext _context;
   

    public Task<Result<Payment>> GetPaymentById(int id)
    {
       try
        {
            var payment = _context.Payments.Include(e=>e.Products).Include(c=>c.Customer).FirstOrDefault(p => p.id == id);
            if (payment == null)
            {
                return Task.FromResult(new Result<Payment>("Payment not found"));
            }
            return Task.FromResult(new Result<Payment>(true)
            {
                Data = new()
                {
                    id = payment.id,
                    CreatedAt = payment.createdAt,
                    Amount = payment.amount,
                    paymentMethod = payment.paymentMethod,
                    userId = payment.userId,
                    customer = new()
                    {
                        id = payment.Customer.id,
                        name = payment.Customer.fullName,
                        phone = payment.Customer.phone,
                        
                    },
                    products = payment.Products.Select(p => new Product()
                    {
                        id = p.id,
                        name = p.name,
                        salePrice = p.salePrice,
                        purchasePrice = p.purchasePrice,
                        image = p.image,
                        description = p.description,
                        quantity = p.quantity,
                    }).ToList()
                }
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new Result<Payment>(ex.Message));
        }
    }

    public Task<Result<List<PaymentShort>>> GetPaymentsListPagenatedByStoreId(int storeId, int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PaymentShort>> ProcessPaymentAsync(int storeId, int userId, NewPayment payment)
    {
        try
        {
            _context.Customers.Add(
                new(){
                    storeId=1,
                    userId=1,
                    fullName="Anvaer",
                    phone="+998921112233",
                    info="hjshjdj",
                }
            );
            _context.SaveChanges();
            var customer = _context.Customers.FirstOrDefault(c => c.id == payment.customerId);
            if (customer == null)
            {
                return Task.FromResult(new Result<PaymentShort>("Customer not found"));
            }
            var products = _context.Products.Where(p => payment.poducts.Select(x => x.id).Contains(p.id)).ToList();
            List<Data.Entities.PaymentItem> paymentItems = new List<Data.Entities.PaymentItem>();
            var newPayment = _context.Payments.Add(
                new(){
                    createdAt = DateTime.UtcNow,
                    amount = products.Sum(e=>e.salePrice),
                    userId=userId,
                    storeId=storeId,
                    paymentMethod=payment.paymentMethod,
                    customerId=payment.customerId,
                    Products = products
                }
            );
            _context.SaveChanges();

            for (int i = 0; i < products.Count; i++)
            {
                paymentItems.Add(new(){
                    paymentId=newPayment.Entity.id,
                    productId=products[i].id,
                    quantity=payment.poducts[i].Quantity,
                    price=products[i].salePrice,
                    totalPrice=payment.poducts[i].Quantity*products[i].salePrice,
                    description="Succed",
                });
                products[i].quantity-=payment.poducts[i].Quantity;

            }
            _context.PaymentItems.AddRange(paymentItems);
            _context.Products.UpdateRange(products);
            _context.SaveChanges();
            
            if (products == null || products.Count == 0)
            {
                return Task.FromResult(new Result<PaymentShort>("Products not found"));
            }
            
            _context.SaveChanges();
            return Task.FromResult(new Result<PaymentShort>(true)
            {
                Data = new()
                {
                    Id = newPayment.Entity.id,
                    CreatedAt = newPayment.Entity.createdAt,
                    Amount = newPayment.Entity.amount,
                    PaymentMethod = newPayment.Entity.paymentMethod,
                }
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new Result<PaymentShort>(ex.Message));
        }
        throw new NotImplementedException();
    }
}
