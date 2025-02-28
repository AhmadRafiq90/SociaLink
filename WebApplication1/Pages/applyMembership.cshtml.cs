using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System.IO;
using WebApplication1.DriveApi;
using WebApplication1.Models;
using WebApplication1.DBcontext;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class Index1Model : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            string? name = Request.Form["name"];
            string? description = Request.Form["description"];
            string? email = Request.Form["email"];
            var uploadedFile = Request.Form.Files["imageFile"];
            if (uploadedFile != null)
            {
                // Assuming you already have a DriveService object for interacting with the API
                DriveService service = new DriveServiceFactory().getInstance();
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = name, // The name of the file on Google Drive
                    Parents = new[] { "1vAlQxW1Ca5pjxfDIJnCJJUdm7e68nY1P" }
                };
                FilesResource.CreateMediaUpload request;

                using (var stream = uploadedFile.OpenReadStream())
                {
                    request = service.Files.Create(
                        fileMetadata, stream, "application/octet-stream");
                    request.Fields = "id, webViewLink";
                    request.Upload();
                }
                var file = request.ResponseBody;
                Console.WriteLine("File ID: " + file.Id);

                // Set the permissions of the file to make it viewable
                var permission = new Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };
                var requestPermission = service.Permissions.Create(permission, file.Id);
                requestPermission.Execute();

                string? url = file.WebViewLink;

                FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
                factory.CreateDbContext(Array.Empty<string>());
                SocietyService sc = new SocietyService(factory.GetContext());
                List<Society>Societies = sc.GetSocieties();

                string id = Societies.Count.ToString();
                Society society = new Society();
                society.Id = Guid.NewGuid().ToString();
                society.Name = name;
                society.Description = description;
                society.LogoURL = url;
                society.ContactEmail = email;
                society.IsApproved = 0;
                sc.AddSociety(society);
            }
        }
    }
}



    





