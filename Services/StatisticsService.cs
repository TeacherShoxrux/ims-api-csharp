using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imsapi.Data;
using imsapi.DTO;

namespace imsapi.Services
{
    public class StatisticsService:IStatiscsService
    {
        private readonly AppDbContext _context;
        public StatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Result<object>> GetStoreTotalByIdAsync(int storeId)
        {
            try
            {
                var totalSales = _context.Payments.Where(s => s.storeId == storeId).Sum(s => s.amount);
                var totalProduct = _context.Products.Where(s => s.storeId == storeId).Count();
                var totalCustomer = _context.Customers.Where(s => s.storeId == storeId).Count();
                var topProduct = _context.Products.Where(s => s.storeId == storeId).OrderByDescending(s => s.quantity).FirstOrDefault();
                return Task.FromResult(new Result<object>(true) { Data = new { 
                    totalSales=totalSales, 
                    totalProduct=totalProduct,
                    totalCustomer=totalCustomer,
                    topProduct=topProduct.name,
                    topProductQuantity=topProduct.quantity,
                    } });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<object>("Error fetching total: " + ex.Message));
            }
        }

        public Task<Result<object>> GetStoreByIdAsync(int storeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var sales = _context.Payments.Where(s => s.storeId == storeId && s.createdAt >= startDate && s.createdAt <= endDate).ToList();
                return Task.FromResult(new Result<object>(true) { Data = sales });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Result<object>("Error fetching sales: " + ex.Message));
            }
        }
  
    
    }
}