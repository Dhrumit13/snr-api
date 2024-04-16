using FakeItEasy;
using SNR_Business.Common.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNR.Test.Controllers
{
    public class BookingControllerTest
    {
        private readonly IMediator _mediator;
        public BookingControllerTest() 
        {
            _mediator = A.Fake<IMediator>();
        }
    }
}
