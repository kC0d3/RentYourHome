using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

namespace RentYourHome.Controllers;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    private readonly string uploadDirectory = "C:/RentYourHome/Images";
    private readonly string zipDirectory = "C:/RentYourHome/Zips";

    [HttpPost("uploads")]
    public IActionResult UploadImages(IFormFileCollection files)
    {
        if (files != null && files.Count > 0)
        {
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
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
                    string filePath = Path.Combine(uploadDirectory, fileName);

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

    [HttpPost("upload")]
    public IActionResult UploadImage(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            try
            {
                if (!IsImageFile(file))
                {
                    return BadRequest(new { Message = "Invalid file format. Please upload an image file." });
                }

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok(new { Message = "Image uploaded successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error: " + ex.Message });
            }
        }
        else
        {
            return BadRequest(new { Message = "Please select an image file to upload." });
        }
    }

    [HttpGet("{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        string imagePath = Path.Combine(uploadDirectory, imageName);

        if (System.IO.File.Exists(imagePath))
        {
            var imageFileStream = System.IO.File.OpenRead(imagePath);
            return File(imageFileStream, "image/jpeg");
        }
        else
        {
            return NotFound();
        }
    }

    /*[HttpGet("{imageNames}")]
    public IActionResult GetImages(string imageNames)
    {
        List<string> imageNameList = imageNames.Split(' ').ToList();

        List<byte[]> images = new List<byte[]>();

        foreach (var imageName in imageNameList)
        {
            var imagePath = Path.Combine(uploadDirectory, imageName);

            if (System.IO.File.Exists(imagePath))
            {
                var imageFileStream = System.IO.File.ReadAllBytes(imagePath);
                images.Add(imageFileStream);
            }
        }

        if (images.Any())
        {
            var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                for (int i = 0; i < images.Count; i++)
                {
                    var entry = archive.CreateEntry($"image_{i}.jpg");
                    using (var entryStream = entry.Open())
                    {
                        entryStream.Write(images[i], 0, images[i].Length);
                    }
                }
            }

            zipStream.Seek(0, SeekOrigin.Begin);

            if (!Directory.Exists(zipDirectory))
            {
                Directory.CreateDirectory(zipDirectory);
            }

            return File(zipStream, zipDirectory, "images.zip");
        }
        else
        {
            return NotFound();
        }
    }*/

    private static bool IsImageFile(IFormFile file)
    {
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(fileExtension);
    }
}