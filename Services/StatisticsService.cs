using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using imsapi.Data;
using imsapi.DTO;
using Microsoft.EntityFrameworkCore;

namespace imsapi.Services
{
    public class StatisticsService : IStatiscsService
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

        public async Task<Result<MemoryStream >> ExportStoreTotalByIdAsync(int storeId)
        {
            try
            {
                var products =await _context.Products.Include(w=>w.Category).ToListAsync();
                   using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Products");
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Name";
                    worksheet.Cell(1, 3).Value = "Description";
                    worksheet.Cell(1, 4).Value = "Category";
                    worksheet.Cell(1, 5).Value = "Sale Price";
                    worksheet.Cell(1, 6).Value = "Purchase Price";
                    worksheet.Cell(1, 7).Value = "Quantity";
                for (int i = 0; i < products.Count; i++)
                    {
                    var p = products[i];
                    worksheet.Cell(i + 2, 1).Value = p.id;
                    worksheet.Cell(i + 2, 2).Value = p.name;
                    worksheet.Cell(i + 2, 3).Value = p.description;
                    worksheet.Cell(i + 2, 4).Value = p.Category?.name;
                    worksheet.Cell(i + 2, 5).Value = p.salePrice;
                    worksheet.Cell(i + 2, 6).Value = p.purchasePrice;
                    worksheet.Cell(i + 2, 7).Value = p.quantity;
                    }
                    var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return new(true){
                        Data=stream
                };
            }
            
            catch (System.Exception)
            {
                
                throw;
            }
            throw new NotImplementedException();
        }
    }
}