using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SNR_Business.Common.Handler;
using SNR_Business.Customer;

namespace SNR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult GetCustomer()
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetCustomerQuery()));
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int? id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetCustomerQuery {empId = id }));
        }
        [HttpPost]
        public IActionResult AddUpdateCustomer([FromBody] AddUpdateCustomerCommand cmd)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<AddUpdateCustomerCommandResult>(cmd));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<DeleteCustomerCommandResult>(new DeleteCustomerCommand {empId = id }));
        }
    }
}
