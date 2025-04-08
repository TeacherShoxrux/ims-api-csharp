namespace imsapi.Services;
public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task<bool> DeleteImageAsync(string fileName);
}