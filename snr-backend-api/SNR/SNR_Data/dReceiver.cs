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
    public interface IdReceiver
    {
        int AddUpdateReceiver(ReceiverEntity req);
        DataSet GetReceiver(int? receiverId);
        int DeleteReceiver(int receiverId);
    }

    public class dReceiver : IdReceiver
    {
        private readonly ISQLHelper _helper;
        public dReceiver(ISQLHelper helper)
        {
            _helper = helper;
        }
        public int AddUpdateReceiver(ReceiverEntity req)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@ReceiverId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.receiverId
            });
            param.Add(new SqlParameter("@ReceiverName", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.receiverName
            });
            param.Add(new SqlParameter("@Email", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.email
            });
            param.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.mobile
            });
           
            param.Add(new SqlParameter("@Address", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.address
            });
            param.Add(new SqlParameter("@City", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.city
            });
            param.Add(new SqlParameter("@IsActive", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Input,
                Value = req.isActive
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spReceiver_Insert_Update");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet GetReceiver(int? receiverId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ReceiverId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = receiverId
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spReceiver_GetAll_ById");
        }

        public int DeleteReceiver(int receiverId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@ReceiverId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = receiverId
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spReceiver_Delete");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }
    }
}
