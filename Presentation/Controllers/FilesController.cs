using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController: ControllerBase
    {
        [HttpPost("upload")]  //Belirtmeden sadece (post - upload; get - download) denebilir
        public async Task<IActionResult> Upload(IFormFile file)
        {
            //Bu tür işlemlerde dosyalar bulutta depolanır. Servis kullanılır

            if (!ModelState.IsValid) //File name is null/invalid
                return BadRequest();

            //folder
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            //path
            var path = Path.Combine(folder, file?.FileName);

            //stream: işlemleri maliyetli işlemlerdir. Onun için using blokları içine yazılır.
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //response body
            return Ok(new
            {
                file = file.FileName,
                path = path,
                size = file.Length
            }) ;
        }
    }
}
