﻿using System;
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
            if (!ds.IsNullOrEmpty()) {
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
                        bookingDate = dr["BookingDate"].ObjToNullableDateTime()
                    });
                }
                res.Booking = lstBooking.ToArray();
            }
            return res;
        }
    }
}
