using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace WebApplication1.DriveApi
{
    public class DriveServiceFactory
    {
        GoogleCredential? credential = null;
        DriveService? service = null;
        public DriveService getInstance()
        {
            if (service == null)
                initInstance();
            return service;
        }
       
        private void initInstance()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/keys/neural-stacker-417908-bb949e449e8f.json");
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(DriveService.Scope.Drive);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API .NET Quickstart",
            });
        }
    }
}




