using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PingDong.Kata.Request;
using PingDong.Kata.Service;

namespace Kata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;
        private readonly IPricingService _service;
        private readonly IValidator<PriceRequest> _validator;

        public PriceController(
            IPricingService service,
            IValidator<PriceRequest> validator,
            ILogger<PriceController> logger)
        {
            _service = service;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PricingAsync(PriceRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var data = await _service.CalculateAsync(request.Items);

            return Ok(data);
        }
    }
}
