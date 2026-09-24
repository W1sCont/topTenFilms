using System.ComponentModel.DataAnnotations;
namespace topTenFilms.Annotations
{
    public class MyAttribute(string[] extensions) : ValidationAttribute
    {
        private readonly string[] _extensions = extensions;
        public override bool IsValid(object? value)
        {
            if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                return _extensions.Contains(extension.ToLowerInvariant());
            }
            return false;
        }
    }
}
