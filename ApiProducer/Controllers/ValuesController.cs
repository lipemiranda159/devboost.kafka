using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IProducer _producer;
        public ValuesController(IProducer producer)
        {
            _producer = producer;
        }
        [HttpPost]
        public async Task<ActionResult> SendData([FromQuery] string data)
        {

            return Ok(await _producer.SendData("teste", data));
        }

    }

}

