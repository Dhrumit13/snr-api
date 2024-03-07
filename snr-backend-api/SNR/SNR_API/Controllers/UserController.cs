using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SNR_Business.Common.Handler;
using SNR_Business.User;

namespace SNR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetUserQuery()));
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int? id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetUserQuery { UserId = id}));
        }
        [HttpPost]
        public IActionResult AddUpdateUser([FromBody] AddUpdateUserCommand cmd)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<AddUpdateUserCommandResult>(cmd));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<DeleteUserCommandResult>(new DeleteUserCommand {userId = id }));
        }
    }
}
