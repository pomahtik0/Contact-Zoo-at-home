using System.ComponentModel.DataAnnotations;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(fileExtension.ToLower()))
            {
                return new ValidationResult($"Invalid file extension. Allowed extensions: {string.Join(", ", _extensions)}");
            }
        }
        return ValidationResult.Success;
    }
}