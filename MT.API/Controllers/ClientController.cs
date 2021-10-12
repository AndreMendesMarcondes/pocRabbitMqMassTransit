using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MT.Domain.Models;

namespace MT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger, IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientModel model)
        {
            _logger.LogInformation($"Send {nameof(ClientModel)}");
            await _publisher.Publish<ClientModel>(model);

            return Ok();
        }
    }
}
