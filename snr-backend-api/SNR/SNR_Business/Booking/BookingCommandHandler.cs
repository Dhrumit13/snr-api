using SNR_Business.Common.Handler;
using SNR_Business.Customer;
using SNR_Data;
using SNR_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SNR_Business.Booking
{
    public class BookingCommand : ICommandWithResponse<BookingCommandResult>
    {
        public long? bookingId { get; set; }
        public BookingCustomer customer { get; set; }
        public BookingReceiver receiver { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public decimal? weight { get; set; }
        public int? quantity { get; set; }
        public decimal? grossAmount { get; set; }
        public string packageType { get; set; }
        public string paymentMode { get; set; }
        public decimal? netAmount { get; set; }
        public string remarks { get; set; }
        public BookingOtherCharges[] otherCharges { get; set; }
    }
    public class BookingCustomer
    {
        public int customerId { get; set; }
    }
    public class BookingReceiver
    {
        public int receiverId { get; set; }
    }
    public class BookingOtherCharges
    {
        public BookingOtherChargeType chargeType { get; set; }
        public decimal value { get; set; }
    }
    public class BookingOtherChargeType
    {
        public int otherChargeId { get; set; }
    }
    public class BookingCommandResult
    {
        public int resFlag { get; set; }
    }
    public class BookingCommandHandler : ICommandHandler<BookingCommand, BookingCommandResult>
    {
        private readonly IdBooking _booking;
        public BookingCommandHandler(IdBooking booking)
        {
            _booking = booking;
        }
        public BookingCommandResult Handle(BookingCommand cmd)
        {
            var req = new BookingEntity();
            req.bookingId = cmd.bookingId;
            req.customerIdf = cmd.customer.customerId;
            req.receiverIdf = cmd.receiver.receiverId;
            req.origin = cmd.origin;
            req.destination = cmd.destination;
            req.weight = cmd.weight;
            req.quantity = cmd.quantity;
            req.grossAmount = cmd.grossAmount;
            req.packageType = cmd.packageType;
            req.paymentMode = cmd.paymentMode;
            req.netAmount = cmd.netAmount;
            req.remarks = cmd.remarks;
            if (cmd.otherCharges != null && cmd.otherCharges.Length > 0)
            { 
                req.otherCharges = new List<BookingChargestbl>();
                foreach (var charge in cmd.otherCharges)
                {
                    req.otherCharges.Add(new BookingChargestbl
                    {
                        OtherChargeId = charge.chargeType.otherChargeId,
                        Value = charge.value
                    });
                }
            }
            var _resFlag = _booking.AddUpdateBooking(req);
            return new BookingCommandResult { resFlag = _resFlag };
        }
    }
}
