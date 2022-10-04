using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace BlogApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService uploadService;
        [HttpPost("upload")]
        public IActionResult Upload([FromForm] Image carImage, [FromForm] IFormFile file)
        {
            var result = uploadService.AddCarImage(carImage, file);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
