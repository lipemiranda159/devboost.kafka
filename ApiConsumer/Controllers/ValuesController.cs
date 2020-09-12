using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ApiConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConsumer _consumer;
        public ValuesController(IConsumer consumer)
        {
            _consumer = consumer;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> StartConsumer()
        {
            await _consumer.ExecuteAsync(default, "teste");
            return Ok(_consumer.GetMessages());
        }
    }
}
