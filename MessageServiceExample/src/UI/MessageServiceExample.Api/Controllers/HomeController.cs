using MessageServiceExample.Core.Consts;
using MessageServiceExample.Core.Models.RequestModels;
using MessageServiceExample.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MessageServiceExample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        IPublisherService _publisherService;
        public HomeController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("send")]
        public IActionResult Send(List<SendMailModel> sendList)
        {
            _publisherService.Enqueue(sendList, RabbitMqConsts.RabbitMqConstsList.QueueNameEmail.ToString());
            return Ok();
        }

    }
}