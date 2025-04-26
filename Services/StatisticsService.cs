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
                return Task.FromResult(new Result<object>(true)
                {
                    Data = new
                    {
                        totalSales = totalSales,
                        totalProduct = totalProduct,
                        totalCustomer = totalCustomer,
                        topProduct = topProduct.name,
                        topProductQuantity = topProduct.quantity,
                    }
                });
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

        public async Task<Result<MemoryStream>> ExportStoreTotalByIdAsync(int storeId)
        {
            try
            {
                var products = await _context.Products.Include(w => w.Category).ToListAsync();
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

                return new(true)
                {
                    Data = stream
                };
            }

            catch (System.Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Result<MemoryStream>> ExportThisMonthSaleTotalByIdAsync(int storeId)
        {
            try
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var startOfNextMonth = startOfMonth.AddMonths(1);

                var products = await _context.PaymentItems.Include(w => w.Product).Where(s => s.createdAt >= startOfMonth && s.createdAt < startOfNextMonth).ToListAsync();
                var totalPrice = await _context.PaymentItems.Include(w => w.Product).Where(s => s.createdAt >= startOfMonth && s.createdAt < startOfNextMonth).SumAsync(s => s.price);

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Products");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nomi";
                worksheet.Cell(1, 3).Value = "Ma'lumoti";
                // worksheet.Cell(1, 4).Value = "Kategoriyasi";
                worksheet.Cell(1, 5).Value = "Sotish narxi";
                worksheet.Cell(1, 6).Value = "Kirish narxi";
                worksheet.Cell(1, 7).Value = "Sotilganlar soni";
                worksheet.Cell(1, 7).Value = "Sotilganlar vaqti";
                for (int i = 0; i < products.Count; i++)
                {
                    var p = products[i];
                    worksheet.Cell(i + 2, 1).Value = p?.Product?.id;
                    worksheet.Cell(i + 2, 2).Value = p?.Product?.name;
                    worksheet.Cell(i + 2, 3).Value = p?.Product?.description;
                    // worksheet.Cell(i + 2, 4).Value = p.Category?.name;
                    worksheet.Cell(i + 2, 5).Value = p?.price;
                    worksheet.Cell(i + 2, 6).Value = p?.Product?.purchasePrice;
                    worksheet.Cell(i + 2, 7).Value = p.quantity;
                    worksheet.Cell(i + 2, 8).Value = p.createdAt.ToShortDateString();
                }
                worksheet.Cell(products.Count + 3, 5).Value = totalPrice;
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return new(true)
                {
                    Data = stream
                };


            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<MemoryStream>> ExportThisWeekSaleTotalByIdAsync(int storeId)
        {
             try
            {
                var today = DateTime.Today;
                int daysSinceMonday = ((int)today.DayOfWeek + 6) % 7; // Dushanbadan boshlanishi uchun
                var startOfWeek = today.AddDays(-daysSinceMonday);
                var endOfWeek = startOfWeek.AddDays(7);

                var products = await _context.PaymentItems.Include(w => w.Product).Where(s => s.createdAt >= startOfWeek && s.createdAt < endOfWeek).ToListAsync();
                var totalPrice = await _context.PaymentItems.Include(w => w.Product).Where(s => s.createdAt >= startOfWeek && s.createdAt < endOfWeek).SumAsync(s => s.price);
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Products");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nomi";
                worksheet.Cell(1, 3).Value = "Ma'lumoti";
                // worksheet.Cell(1, 4).Value = "Kategoriyasi";
                worksheet.Cell(1, 5).Value = "Sotish narxi";
                worksheet.Cell(1, 6).Value = "Kirish narxi";
                worksheet.Cell(1, 7).Value = "Sotilganlar soni";
                worksheet.Cell(1, 7).Value = "Sotilganlar vaqti";
                for (int i = 0; i < products.Count; i++)
                {
                    var p = products[i];
                    worksheet.Cell(i + 2, 1).Value = p?.Product?.id;
                    worksheet.Cell(i + 2, 2).Value = p?.Product?.name;
                    worksheet.Cell(i + 2, 3).Value = p?.Product?.description;
                    // worksheet.Cell(i + 2, 4).Value = p.Category?.name;
                    worksheet.Cell(i + 2, 5).Value = p?.price;
                    worksheet.Cell(i + 2, 6).Value = p?.Product?.purchasePrice;
                    worksheet.Cell(i + 2, 7).Value = p.quantity;
                    worksheet.Cell(i + 2, 8).Value = p.createdAt.ToShortDateString();
                }
                 worksheet.Cell(products.Count + 3, 5).Value = totalPrice;
                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return new(true)
                {
                    Data = stream
                };


            }
            catch (System.Exception ex)
            {

                throw;
            }
        }

        public async Task<Result<MemoryStream>> ExportThisDaySaleTotalByIdAsync(int storeId)
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                var products = await _context.PaymentItems.Include(w => w.Product).Where(s => s.createdAt >= today && s.createdAt < tomorrow).ToListAsync();
                var totalSumm = await _context.PaymentItems.Where(s => s.createdAt >= today && s.createdAt < tomorrow).SumAsync(s => s.price);
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Products");
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nomi";
                worksheet.Cell(1, 3).Value = "Ma'lumoti";
                // worksheet.Cell(1, 4).Value = "Kategoriyasi";
                worksheet.Cell(1, 5).Value = "Sotish narxi";
                worksheet.Cell(1, 6).Value = "Kirish narxi";
                worksheet.Cell(1, 7).Value = "Sotilganlar soni";
                worksheet.Cell(1, 7).Value = "Sotilganlar vaqti";
                for (int i = 0; i < products.Count; i++)
                {
                    var p = products[i];
                    worksheet.Cell(i + 2, 1).Value = p?.Product?.id;
                    worksheet.Cell(i + 2, 2).Value = p?.Product?.name;
                    worksheet.Cell(i + 2, 3).Value = p?.Product?.description;
                    // worksheet.Cell(i + 2, 4).Value = p.Category?.name;
                    // worksheet.Cell(i + 2, 5).Value = p?.Product?.salePrice;
                    worksheet.Cell(i + 2, 6).Value = p?.price;
                    worksheet.Cell(i + 2, 7).Value = p.quantity;
                    worksheet.Cell(i + 2, 8).Value = p.createdAt.ToShortDateString();
                }
                worksheet.Cell(products.Count + 3, 6).Value = totalSumm;

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return new(true)
                {
                    Data = stream
                };


            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}