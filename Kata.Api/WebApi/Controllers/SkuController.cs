using Microsoft.AspNetCore.Mvc;
using PingDong.Kata.Infrastructure;

namespace Kata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkuController : ControllerBase
    {
        private readonly ILogger<SkuController> _logger;
        private readonly ISkuRepository _skuRepository;

        public SkuController(
            ISkuRepository skuRepository,
            ILogger<SkuController> logger)
        {
            _skuRepository = skuRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _skuRepository.GetAllAsync();

            return Ok(data);
        }
    }
}
