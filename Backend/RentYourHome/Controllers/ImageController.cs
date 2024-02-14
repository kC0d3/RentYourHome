using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentYourHome.Controllers;

[Authorize]
[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "AdsImages");

    [HttpPost]
    public IActionResult UploadImages(IFormFileCollection files)
    {
        if (files != null && files.Count > 0)
        {
            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }

            try
            {
                foreach (var file in files)
                {
                    if (!IsImageFile(file))
                    {
                        return BadRequest(new { Message = "Invalid file format. Please upload image files only." });
                    }

                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(_uploadDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok(new { Message = "Images uploaded successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error: " + ex.Message });
            }
        }
        else
        {
            return BadRequest(new { Message = "Please select at least one image file to upload." });
        }
    }

    [HttpGet("{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        string imagePath = Path.Combine(_uploadDirectory, imageName);

        if (System.IO.File.Exists(imagePath))
        {
            var imageFileStream = System.IO.File.OpenRead(imagePath);
            return File(imageFileStream, "image/jpeg");
        }

        return NotFound();
    }

    [HttpPut("{imageName}")]
    public IActionResult UpdateImage(string imageName, IFormFile file)
    {
        if (file == null)
        {
            return BadRequest(new { Message = "Please select an image file to update." });
        }

        if (!IsImageFile(file))
        {
            return BadRequest(new { Message = "Invalid file format. Please upload image files only." });
        }

        string imagePath = Path.Combine(_uploadDirectory, imageName);

        if (!System.IO.File.Exists(imagePath))
        {
            return NotFound();
        }

        try
        {
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok(new { Message = "Image updated successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Error: " + ex.Message });
        }
    }

    [HttpDelete("{imageName}")]
    public IActionResult DeleteImage(string imageName)
    {
        string imagePath = Path.Combine(_uploadDirectory, imageName);

        if (System.IO.File.Exists(imagePath))
        {
            try
            {
                System.IO.File.Delete(imagePath);
                return Ok(new { Message = "Image deleted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error: " + ex.Message });
            }
        }

        return NotFound();
    }

    private static bool IsImageFile(IFormFile file)
    {
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(fileExtension);
    }
}