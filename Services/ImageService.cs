namespace imsapi.Services;
public class ImageService : IImageService
{
    private readonly string _imageDirectory;

    public ImageService(IConfiguration configuration)
    {
      
        _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        // Agar rasm saqlash joyi mavjud bo'lmasa, uni yaratish
        if (!Directory.Exists(_imageDirectory))
        {
            Directory.CreateDirectory(_imageDirectory);
        }
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_imageDirectory, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;  // yoki filePath ni qaytarishingiz mumkin
    }

    public async Task<bool> DeleteImageAsync(string fileName)
    {
        var filePath = Path.Combine(_imageDirectory, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}
