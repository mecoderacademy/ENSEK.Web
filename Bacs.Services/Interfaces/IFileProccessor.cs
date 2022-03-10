using ENSEK.Models.Interfaces;
using ENSEK.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Services.Interfaces
{
    public interface IFileProccessor
    {
        List<IFileResponse> ReadFile(IFormFile formFile);
    }
}
