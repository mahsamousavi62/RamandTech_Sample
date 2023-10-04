using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RamandTech.Dapper.IServices;
using RamandTech.MessageBroker.Rabbitmq;


namespace RamandTech.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public ProducerController(IRabbitMQProducer rabbitMQProducer,
                                    IConfiguration configuration,
                                    IUserRepository userRepository)
        {
            _rabbitMQProducer = rabbitMQProducer;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [MapToApiVersion("1.0")]
        [HttpGet(Name = "SendMessage")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> SendMessage()
        {
            var firstUser = await _userRepository.GetByIdAsync();

            var user = JsonConvert.SerializeObject(firstUser);

            _rabbitMQProducer.SendUserMessage(user,
                queue: _configuration["RabbitMQ:ProdeuceQueue"],
                null);

            return Ok($"first user : {user} is published successfully .");

        }

    }
}
