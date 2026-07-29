 

public class ImageService 
{
    private readonly IWebHostEnvironment _environment; 

    public ImageService(IWebHostEnvironment environment )
    {
        _environment = environment; 
    }

    public async Task<string> SaveImageAsync(IFormFile file, string folder)
    {
        Validate(file);

        string extension = Path.GetExtension(file.FileName);

        string fileName = $"{Guid.NewGuid()}{extension}";

        string uploadFolder = Path.Combine(
            _environment.WebRootPath,
            "Upload",
            folder);

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string fullPath = Path.Combine(uploadFolder, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"Upload/{folder}/{fileName}";
    }

    public async Task DeleteImageAsync(string imagePath)
    {
        var fullPath = Path.Combine(_environment.WebRootPath, imagePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        await Task.CompletedTask;
    }


    private void Validate(IFormFile file)
    {
        if (file == null)
            throw new Exception("Image is required.");

        if (file.Length == 0)
            throw new Exception("Image is empty.");

        if (file.Length > 5 * 1024 * 1024)
            throw new Exception("Maximum file size is 5 MB.");

        string[] extensions = { ".jpg", ".jpeg", ".png", ".webp" };

        string extension = Path.GetExtension(file.FileName).ToLower();

        if (!extensions.Contains(extension))
            throw new Exception("Only JPG, JPEG, PNG and WEBP are allowed.");

        string[] contentTypes =
        {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

        if (!contentTypes.Contains(file.ContentType))
            throw new Exception("Invalid image.");
    }
    //private void Validate(IFormFile file)
    //{
    //    if (file == null)
    //        throw new Exception("Image is required.");

    //    if (file.Length == 0)
    //        throw new Exception("Image file is empty.");

    //    // Maximum 5 MB
    //    const long maxSize = 5 * 1024 * 1024;

    //    if (file.Length > maxSize)
    //        throw new Exception("Image size cannot exceed 5 MB.");

    //    // Allowed Extensions
    //    string[] allowedExtensions =
    //    {
    //        ".jpg",
    //        ".jpeg",
    //        ".png",
    //        ".webp"
    //    };

    //    // Allowed Content Types
    //    string[] allowedContentTypes =
    //    {
    //        "image/jpeg",
    //        "image/png",
    //        "image/webp"
    //    };

    //    string extension = Path.GetExtension(file.FileName).ToLower();

    //    if (!allowedExtensions.Contains(extension))
    //        throw new Exception("Only JPG, JPEG, PNG and WEBP images are allowed.");

    //    if (!allowedContentTypes.Contains(file.ContentType))
    //        throw new Exception("Invalid image content type.");

    //    try
    //    {
    //        using var stream = file.OpenReadStream();
    //        using var image = Image.Load(stream);
    //    }
    //    catch
    //    {
    //        throw new Exception("Invalid image file.");
    //    }
    //}
}