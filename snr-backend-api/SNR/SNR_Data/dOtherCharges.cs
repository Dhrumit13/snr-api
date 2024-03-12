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
    public interface IdOtherCharges
    {
        int AddUpdateOtherCharges(OtherChargesEntity req);
        DataSet GetOtherCharges(int? OtherChargesId);
        int DeleteOtherCharges(int OtherChargesId);
    }

    public class dOtherCharges : IdOtherCharges
    {
        private readonly ISQLHelper _helper;
        public dOtherCharges(ISQLHelper helper)
        {
            _helper = helper;
        }
        public int AddUpdateOtherCharges(OtherChargesEntity req)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@OtherChargeId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.otherChargeId
            });
            param.Add(new SqlParameter("@OtherChargeName", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.otherChargeName
            });
            param.Add(new SqlParameter("@Amount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.amount
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spOtherCharges_Insert_Update");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet GetOtherCharges(int? OtherChargeId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@OtherChargeId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = OtherChargeId
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spOtherCharges_GetAll_ById");
        }

        public int DeleteOtherCharges(int empId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@OtherChargeId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = empId
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spOtherCharges_Delete");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }
    }
}
