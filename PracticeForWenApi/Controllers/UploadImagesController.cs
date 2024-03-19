using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PracticeForWebApi.Models;




public class UploadImagesController : Controller
{
    private readonly IConfiguration _configuration;

    public UploadImagesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //    [HttpPost("UploadFiles")]
    //    public async Task<IActionResult> UploadFiles( IFormFile file)
    //    {
    //        var allowedFileTypes = _configuration["AllowedFileTypes"];
    //        var maxFileSizeBytes = Convert.ToInt32(_configuration["MaxFileSizeBytes"]);
    //        var uploadsFolderPath = _configuration["UploadsFolderPath"];

    //        // Check if a file is uploaded
    //        if (file != null && file.Length > 0)
    //        {
    //            // Check file size
    //            if (file.Length > maxFileSizeBytes)
    //            {
    //                return BadRequest("File size exceeds the maximum allowed limit.");
    //            }

    //            // Check file type
    //            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
    //            if (!allowedFileTypes.Split(',').Any(x => x.Trim() == fileExtension))
    //            {
    //                return BadRequest("File type is not allowed.");
    //            }

    //            // Generate a unique filename
    //            var fileName = Guid.NewGuid().ToString() + fileExtension;

    //            // Construct the full path to save the file
    //            var filePath = Path.Combine(uploadsFolderPath, fileName);

    //            // Save the file
    //            using (var stream = new FileStream(filePath, FileMode.Create))
    //            {
    //                await file.CopyToAsync(stream);
    //            }

    //            // You can return a success response, or do anything else you need
    //            return Ok($"File '{fileName}' uploaded successfully.");
    //        }
    //        else
    //        {
    //            return BadRequest("No file uploaded.");
    //        }
    //    }
    //}





    [HttpPost("UploadFiles")]
    public async Task<IActionResult> UploadFiles(IEnumerable<IFormFile> files)
    {
        if (files == null || !files.Any())
        {
            return BadRequest("No files uploaded.");
        }

        var allowedFileTypes = _configuration["AllowedFileTypes"];
        var maxFileSizeBytes = Convert.ToInt32(_configuration["MaxFileSizeBytes"]);
        var uploadsFolderPath = _configuration["UploadsFolderPath"];

        var uploadedFiles = new List<string>();

        foreach (var file in files)
        {
            // Check file size
            if (file.Length > maxFileSizeBytes)
            {
                return BadRequest($"File '{file.FileName}' size exceeds the maximum allowed limit.");
            }

            // Check file type
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedFileTypes.Split(',').Any(x => x.Trim() == fileExtension))
            {
                return BadRequest($"File type of '{file.FileName}' is not allowed.");
            }

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + fileExtension;

            // Construct the full path to save the file
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Store the uploaded file path
            uploadedFiles.Add(filePath);
        }

        // You can return a success response, or do anything else you need
        return Ok($"Uploaded {files.Count()} files successfully: {string.Join(", ", uploadedFiles)}");
    }
}




