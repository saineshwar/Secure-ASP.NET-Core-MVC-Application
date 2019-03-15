using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreSecurity.Filters;
using CoreSecurity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CoreSecurity.Controllers
{
    public class UploadController : Controller
    {

        private readonly IHostingEnvironment _environment;

        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _environment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Document()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Document(UploadModel uploadModel)
        {
            if (ModelState.IsValid)
            {
                var upload = HttpContext.Request.Form.Files;

                if (HttpContext.Request.Form.Files.Count == 0)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else
                {
                    foreach (var file in upload)
                    {
                        if (file.Length > 0)
                        {

                            byte[] tempFileBytes = null;

                            // getting File Name
                            var fileName = file.FileName.Trim();

                            using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
                            {
                                // getting filebytes
                                tempFileBytes = reader.ReadBytes((int)file.Length);
                            }

                            // Creating new FileName
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            var filetype = Path.GetExtension(fileName).Replace('.', ' ').Trim();

                            // getting FileExtension
                            var fileExtension = Path.GetExtension(fileName);

                            var types = CoreSecurity.Filters.FileUploadCheck.FileType.Image;  // Setting Image type
                            var result = FileUploadCheck.IsValidFile(tempFileBytes, types, filetype); // Validate Header

                            if (result)
                            {
                                var newFileName = string.Concat(myUniqueFileName, fileExtension);
                                fileName = Path.Combine(_environment.WebRootPath, "images") + $@"\{newFileName}";
                                using (FileStream fs = System.IO.File.Create(fileName))
                                {
                                    file.CopyTo(fs);
                                    fs.Flush();
                                }
                            }

                        }
                    }
                }
            }
            return View(uploadModel);
        }
    }
}