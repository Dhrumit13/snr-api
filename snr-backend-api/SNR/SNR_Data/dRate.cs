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
    public interface IdRate
    {
        int AddUpdateRate(RateEntity req);
        DataSet GetRate(int? rateId, int? customerId);
        int DeleteRate(int rateId);
    }

    public class dRate : IdRate
    {
        private readonly ISQLHelper _helper;
        public dRate(ISQLHelper helper)
        {
            _helper = helper;
        }
        public int AddUpdateRate(RateEntity req)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@RateId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.rateId
            });
            param.Add(new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.customerId
            });
            param.Add(new SqlParameter("@TransportationMode", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.transportationMode
            });
            param.Add(new SqlParameter("@City", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.city
            });
            param.Add(new SqlParameter("@MinWeight", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.minWeight
            });
            param.Add(new SqlParameter("@RatePerKg", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.ratePerKg
            });
            param.Add(new SqlParameter("@RatePerPiece", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.ratePerPiece
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spRate_Insert_Update");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet GetRate(int? rateId, int? customerId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@RateId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = rateId
            });
            param.Add(new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = customerId
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spRate_GetAll_ById");
        }

        public int DeleteRate(int empId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@RateId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = empId
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spRate_Delete");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }
    }
}
