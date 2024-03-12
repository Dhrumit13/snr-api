using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SNR_Business.Common.Handler;
using SNR_Business.Receiver;

namespace SNR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReceiverController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult GetReceiver()
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetReceiverQuery()));
        }
        [HttpGet("{id}")]
        public IActionResult GetReceiver(int? id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetReceiverQuery { custId = id }));
        }
        [HttpPost]
        public IActionResult AddUpdateReceiver([FromBody] AddUpdateReceiverCommand cmd)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<AddUpdateReceiverCommandResult>(cmd));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReceiver(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<DeleteReceiverCommandResult>(new DeleteReceiverCommand {receiverId = id }));
        }
    }
}
