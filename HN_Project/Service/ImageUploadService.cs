 

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
        string uploadFolder = Path.Combine(_environment.WebRootPath, "Upload", folder);
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

        if (file.Length > 2 * 1024 * 1024)
            throw new Exception("Maximum file size is 2 MB.");

        string[] extensions = { ".jpg", ".jpeg", ".png", ".webp" };

        string extension = Path.GetExtension(file.FileName).ToLower();

        if (!extensions.Contains(extension))
            throw new Exception("Only JPG, JPEG, PNG and WEBP are allowed.");

        string[] contentTypes = { "image/jpeg", "image/png", "image/webp" };

        if (!contentTypes.Contains(file.ContentType))
            throw new Exception("Invalid image.");
    } 
}