using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SNR_Business.Common.Handler;
using SNR_Business.OtherCharges;
using SNR_Business.Rate;

namespace SNR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherChargesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OtherChargesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult GetOtherCharges([FromQuery] GetOtherChargesQuery query)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(query));
        }
        [HttpPost]
        public IActionResult AddUpdateOtherCharges([FromBody] AddUpdateOtherChargesCommand cmd)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<AddUpdateOtherChargesCommandResult>(cmd));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOtherCharges(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<DeleteOtherChargesCommandResult>(new DeleteOtherChargesCommand {OtherChargeId = id }));
        }
    }
}
