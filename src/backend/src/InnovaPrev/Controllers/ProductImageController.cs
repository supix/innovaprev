using DomainModel.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InnovaPrev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseCache(NoStore = false, Duration = 600)]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageProvider productImageProvider;

        public ProductImageController(IProductImageProvider productImageProvider)
        {
            this.productImageProvider = productImageProvider ?? throw new ArgumentNullException(nameof(productImageProvider));
        }

        // GET api/<ProductImageController>/IPC?isThumb=true
        [HttpGet("{id}")]
        public IActionResult Get(string id, bool isThumb)
        {
            return File(this.productImageProvider.Get(id, isThumb), "image/jpeg");
        }
    }
}
