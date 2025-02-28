using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DBcontext;
using WebApplication1.Models;
using WebApplication1.DriveApi;

namespace bh2.Pages
{
    public class society_profileModel : PageModel
    {
        public Society? Society { get; set; }
        
        public string imageLink { get; set; }
        public void OnGet(string id)
        {
            FASTSocietyManagementContextFactory factory = new FASTSocietyManagementContextFactory();
            var context = factory.GetContext();
            Society = context.Societies.Find(id);

            if (Society != null)
            {
                string url = Society.LogoURL;
                string fileId = url.Substring(url.IndexOf("/d/") + 3); // +3 to skip "/d/"
                fileId = fileId.Substring(0, fileId.IndexOf("/view")); // Get the part before "/view"

                DriveServiceFactory driveServiceFactory = new DriveServiceFactory();
                var driveService = driveServiceFactory.getInstance();
                var request = driveService.Files.Get(fileId);
                request.Fields = "webContentLink";
                var file = request.Execute();
                imageLink = file.WebContentLink;

                Console.WriteLine(imageLink); 
            }
        }
    }
}
