namespace SelectAndRepair.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ImageFileValidateAttribute : RequiredAttribute
    {
        private const int MaxFileLengthInBytes = 5 * 1024 * 1024; // 5 MB;

        public override bool IsValid(object value)
        {
            IFormFile file = value as IFormFile;

            if (file == null)
            {
                return false;
            }

            if (file.Length > MaxFileLengthInBytes)
            {
                return false;
            }

            // Check the image mime type
            if (file.ContentType.ToLower() != "image/jpg"
                && file.ContentType.ToLower() != "image/jpeg"
                && file.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            return true;
        }
    }
}
