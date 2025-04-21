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

    public Task<Result<List<PaymentShort>>> GetPaymentsListPagenatedByStoreId(int storeId, int page=1, int pageSize=10)
    {
        try{
            var payments = _context.Payments
                .Include(c => c.Customer)
                .Include(c => c.User)
                .Where(p => p.storeId == storeId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (payments == null || payments.Count == 0)
            {
                return Task.FromResult(new Result<List<PaymentShort>>("Payments not found"));
            }

            var result = payments.Select(p => new PaymentShort()
            {
                Id = p.id,
                CreatedAt = p.createdAt,
                Amount = p.amount,
                PaymentMethod = p.paymentMethod,
                customerName = p.Customer.fullName,
                userFullName=p.User.fullName,
                customerPhone = p.Customer.phone,
                customerId = p.Customer.id,

            }).ToList();

            return Task.FromResult(new Result<List<PaymentShort>>(true)
            {
                Data = result
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new Result<List<PaymentShort>>(ex.Message));
        }
    }

   public Task<Result<PaymentShort>> ProcessPaymentAsync(int storeId, int userId, NewPayment payment)
{
    try
    {
        var customer = _context.Customers.FirstOrDefault(c => c.id == payment.customerId);
        if (customer == null)
        {
            return Task.FromResult(new Result<PaymentShort>("Mijoz topilmadi"));
        }

        var products = _context.Products
            .Where(p => payment.poducts.Select(x => x.id).Contains(p.id)).ToList();

        if (products == null || products.Count == 0)
        {
            return Task.FromResult(new Result<PaymentShort>("Mahsulot topilmadi"));
        }

        // Check if quantities are sufficient
        for (int i = 0; i < products.Count; i++)
        {
            var ordered = payment.poducts.FirstOrDefault(x => x.id == products[i].id);
            if (ordered == null || products[i].quantity < ordered.Quantity)
            {
                return Task.FromResult(new Result<PaymentShort>($"Mahsulot '{products[i].name}' etarli miqdorda emas."));
            }
        }

        var newPayment = _context.Payments.Add(new()
        {
            createdAt = DateTime.UtcNow,
            amount = products.Sum(e => e.salePrice),
            userId = userId,
            storeId = storeId,
            paymentMethod = payment.paymentMethod??"",
            customerId = payment.customerId,
            Products = products
        });

        _context.SaveChanges();

        List<Data.Entities.PaymentItem> paymentItems = new();

        for (int i = 0; i < products.Count; i++)
        {
            var ordered = payment.poducts.First(x => x.id == products[i].id);

            paymentItems.Add(new()
            {
                paymentId = newPayment.Entity.id,
                productId = products[i].id,
                quantity = ordered.Quantity,
                price = products[i].salePrice,
                totalPrice = ordered.Quantity * products[i].salePrice,
                description = "Muvaffaiyatli",
            });

            products[i].quantity -= ordered.Quantity;
        }

        _context.PaymentItems.AddRange(paymentItems);
        _context.Products.UpdateRange(products);
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
}

}
