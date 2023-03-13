using Microsoft.AspNetCore.Mvc;

namespace gamerszone.Controller
{
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

    

        [HttpPost("upload/issues")]
        public  string Issues(IFormFile file)
        {
            try
            {
                UploadFile(file);
                
                return file.FileName.ToString();
            }
            catch(Exception ex)
            {
                return "Failed";
                
            }

        }

        public async Task UploadFile(IFormFile file)
        {
            if(file != null && file.Length > 0) 
            {
                var imagePath = @"\Upload";
                var uploadPath = _environment.WebRootPath + imagePath;
                if(!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var fullPath = Path.Combine(uploadPath, file.FileName);
                using(FileStream fileStream = new FileStream(fullPath,FileMode.Create,FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

    }
}
