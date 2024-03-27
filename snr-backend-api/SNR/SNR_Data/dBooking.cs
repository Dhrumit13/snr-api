using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SNR_Entities;
using SNR_Data.Util;

namespace SNR_Data
{
    public interface IdBooking
    {
        int AddUpdateBooking(BookingEntity req);
        DataSet GetBooking(long? bookingId);
    }
    public class dBooking : IdBooking
    {
        private readonly ISQLHelper _helper;
        public dBooking(ISQLHelper helper)
        {
            _helper = helper;
        }

        public int AddUpdateBooking(BookingEntity req)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@BookingId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Input,
                Value = req.bookingId
            });
            param.Add(new SqlParameter("@CustomerIdf", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.customerIdf
            });
            param.Add(new SqlParameter("@ReceiverIdf", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.receiverIdf
            });
            param.Add(new SqlParameter("@Origin", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.origin
            });
            param.Add(new SqlParameter("@Destination", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.destination
            });
            param.Add(new SqlParameter("@Weight", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = req.weight
            });
            param.Add(new SqlParameter("@Quantity", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.quantity
            });
            param.Add(new SqlParameter("@GrossAmount", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = req.grossAmount
            });
            param.Add(new SqlParameter("@PackageType", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.packageType
            });
            param.Add(new SqlParameter("@PaymentMode", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.paymentMode
            });
            param.Add(new SqlParameter("@NetAmount", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Input,
                Value = req.netAmount
            });
            param.Add(new SqlParameter("@Remarks", SqlDbType.VarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.remarks
            });
            param.Add(new SqlParameter("@Charges", SqlDbType.Structured)
            {
                Direction = ParameterDirection.Input,
                Value = req.charges.ToBookingChargestbl()
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spBooking_Insert_Update");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet GetBooking(long? bookingId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@BookingId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Input,
                Value = bookingId
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spBooking_GetAll_ById");
        }

    }
}
