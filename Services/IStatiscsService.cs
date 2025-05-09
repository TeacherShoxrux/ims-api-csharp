using ClosedXML.Excel;
using imsapi.DTO;

namespace imsapi.Services;
public interface IStatiscsService
{
    Task<Result<object>> GetStoreTotalByIdAsync(int storeId);
    Task<Result<MemoryStream>> ExportStoreTotalByIdAsync(int storeId);
    Task<Result<MemoryStream>> ExportThisMonthSaleTotalByIdAsync(int storeId);
    Task<Result<MemoryStream>> ExportThisWeekSaleTotalByIdAsync(int storeId); 
    Task<Result<MemoryStream>> ExportThisDaySaleTotalByIdAsync(int storeId); 
    Task<Result<object>> GetStoreByIdAsync(int storeId, DateTime startDate, DateTime endDate);
}