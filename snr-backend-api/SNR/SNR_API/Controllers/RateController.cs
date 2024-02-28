using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SNR_Business.Common.Handler;
using SNR_Business.Rate;

namespace SNR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult GetRate()
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetRateQuery()));
        }
        [HttpGet("{id}")]
        public IActionResult GetRate(int? id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetRateQuery { rateId = id }));
        }
        [HttpPost]
        public IActionResult AddUpdateRate([FromBody] AddUpdateRateCommand cmd)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<AddUpdateRateCommandResult>(cmd));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRate(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<DeleteRateCommandResult>(new DeleteRateCommand {rateId = id }));
        }
    }
}
