using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNR_Business.Common.Handler;
using SNR_Business.Common.Util;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Customer
{
    public class GetBookingQuery : IQuery<GetBookingQueryResult>
    {
        public long? bookingId { get; set; }
    }
    public class GetBookingQueryResult
    {
        public BookingEntity[] Booking { get; set; }
    }
    public class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, GetBookingQueryResult>
    {
        private readonly IdBooking _booking;
        public GetBookingQueryHandler(IdBooking booking)
        {
            _booking = booking;
        }

        public GetBookingQueryResult Get(GetBookingQuery query)
        {
            var res = new GetBookingQueryResult();
            var ds = _booking.GetBooking(query.bookingId);
            if (!ds.IsNullOrEmpty())
            {
                var lstOtherCharges = new List<BookingChargestbl>();
                if (ds.Tables.Count > 1 && !ds.Tables[1].IsNullOrEmpty())
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        lstOtherCharges.Add(new BookingChargestbl
                        {
                            bookingId = dr["BookingId"].ObjToNullableLong(),
                            otherChargeId = dr["OtherChargeId"].ObjToInt32(),
                            value = dr["Value"].ObjToDecimal(),
                            chargeType = new ChargeType
                            {
                                otherChargeId = dr["OtherChargeId"].ObjToInt32(),
                                otherChargeName = dr["otherChargeName"].ToString(),
                                amount = dr["amount"].ObjToDecimal()
                            }
                        });
                    }
                }



                List<BookingEntity> lstBooking = new List<BookingEntity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstBooking.Add(new BookingEntity
                    {
                        bookingId = dr["BookingId"].ObjToNullableLong(),
                        customerIdf = dr["CustomerIdf"].ObjToNullableInt32(),
                        receiverIdf = dr["ReceiverIdf"].ObjToNullableInt32(),
                        origin = dr["Origin"].ToString(),
                        destination = dr["Destination"].ToString(),
                        weight = dr["Weight"].ObjToNullableDecimal(),
                        quantity = dr["Quantity"].ObjToNullableInt32(),
                        grossAmount = dr["GrossAmount"].ObjToNullableDecimal(),
                        packageType = dr["PackageType"].ToString(),
                        paymentMode = dr["PaymentMode"].ToString(),
                        netAmount = dr["NetAmount"].ObjToNullableDecimal(),
                        remarks = dr["Remarks"].ToString(),
                        bookingDate = dr["BookingDate"].ObjToNullableDateTime(),
                        otherCharges = lstOtherCharges.Where(x => x.bookingId == dr["BookingId"].ObjToNullableLong()).ToList(),
                        customer = new CustomerEntity
                        {
                            customerId = dr["customerId"].ObjToNullableInt32(),
                            name = dr["name"].ToString(),
                            email = dr["email"].ToString(),
                            mobile = dr["mobile"].ToString(),
                            gstNo = dr["gstNo"].ToString(),
                            address = dr["address"].ToString(),
                            city = dr["city"].ToString(),
                            state = dr["state"].ToString(),
                            cgst = dr["cgst"].ObjToNullableFloat(),
                            sgst = dr["sgst"].ObjToNullableFloat(),
                            igst = dr["igst"].ObjToNullableFloat(),
                            isActive = dr["isActive"].ObjToNullableBool()
                        },
                        receiver = new ReceiverEntity
                        {
                            receiverId = dr["receiverId"].ObjToNullableInt32(),
                            receiverName = dr["receiverName"].ToString(),
                            email = dr["email"].ToString(),
                            mobile = dr["mobile"].ToString(),
                            address = dr["address"].ToString(),
                            city = dr["city"].ToString(),
                            isActive = dr["isActive"].ObjToNullableBool()
                        }

                    });
                }
                res.Booking = lstBooking.ToArray();
            }
            return res;
        }
    }
}
