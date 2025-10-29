using Bloggie.web.Repositories; //Name of the interface
using CloudinaryDotNet; // Required for Account and Cloudinary classes
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration; // Required for IConfiguration
using System.Net;
using System.Threading.Tasks;

namespace Bloggie.web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account; //CloudinaryDotNet.Account

        // 2. Constructor for Dependency Injection
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            // 3. Initialize the Cloudinary Account object using appsettings.json values
            // configuration.GetSection retrieves the "Cloudinary" section
            this.account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
            );
        }

        // Implements the interface method
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            //Call the asynchronous upload method
            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }
    }
}