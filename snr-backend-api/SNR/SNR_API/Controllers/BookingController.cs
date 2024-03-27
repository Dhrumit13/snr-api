using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNR_Business.Booking;
using SNR_Business.Common.Handler;
using SNR_Business.Customer;
using System.Net;

namespace SNR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public IActionResult AddUpdateBooking([FromBody] BookingCommand cmd)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Execute<BookingCommandResult>(cmd));
        }
        [HttpGet]
        public IActionResult GetBooking()
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetBookingQuery()));
        }
        [HttpGet("{id}")]
        public IActionResult GetBooking(long? id)
        {
            return StatusCode((int)HttpStatusCode.OK, _mediator.Query(new GetBookingQuery { bookingId = id }));
        }
    }
}
