

using ENSEK.Models.Models;
using ENSEK.Services.Constants;
using ENSEK.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENSEK.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileProccessorController : ControllerBase
    {

        private readonly IFileProccessor _fileProccessor;
        public FileProccessorController(IFileProccessor fileProccessor)
        {
            _fileProccessor = fileProccessor;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public IActionResult OnPostUpload(IFormFile fileToUpload)
        {
            if (fileToUpload == null) return BadRequest(new FileResponseBadValidation() { ResponseMessage = Constants.NoFiles }); // could test all edge cases
            var result = _fileProccessor.ReadFile(fileToUpload);    
            return Ok(result);
        }
    }
}
