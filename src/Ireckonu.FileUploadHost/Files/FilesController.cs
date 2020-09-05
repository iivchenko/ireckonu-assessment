using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ireckonu.FileUploadHost.Files
{
    [Route("api/files")]
    [ApiController]
    public sealed class FilesController : ControllerBase
    {
        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
