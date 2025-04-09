using imsapi.DTO;
using imsapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace imsapi.Controllers;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[Authorize]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    // Rasmni yuklash
    [Consumes("multipart/form-data")]
    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] ImageUpload image)
    {
        if (image.image == null)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var fileName = await _imageService.UploadImageAsync(image.image);
            return Ok(new { FileName = fileName });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Rasmni o'chirish
    [HttpDelete("delete/{fileName}")]
    public async Task<IActionResult> DeleteImage(string fileName)
    {
        try
        {
            var result = await _imageService.DeleteImageAsync(fileName);
            if (result)
            {
                return Ok(new { Message = "File deleted successfully." });
            }
            else
            {
                return NotFound("File not found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
