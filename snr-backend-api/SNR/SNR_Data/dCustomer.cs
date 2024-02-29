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
    public interface IdCustomer
    {
        int AddUpdateCustomer(CustomerEntity req);
        DataSet GetCustomer(int? custId);
        int DeleteCustomer(int custId);
    }

    public class dCustomer : IdCustomer
    {
        private readonly ISQLHelper _helper;
        public dCustomer(ISQLHelper helper)
        {
            _helper = helper;
        }
        public int AddUpdateCustomer(CustomerEntity req)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.customerId
            });
            param.Add(new SqlParameter("@Name", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.name
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
            param.Add(new SqlParameter("@GstNo", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.gstNo
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
            param.Add(new SqlParameter("@State", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.state
            });
            param.Add(new SqlParameter("@Cgst", SqlDbType.Float)
            {
                Direction = ParameterDirection.Input,
                Value = req.cgst
            });
            param.Add(new SqlParameter("@Sgst", SqlDbType.Float)
            {
                Direction = ParameterDirection.Input,
                Value = req.sgst
            });
            param.Add(new SqlParameter("@Igst", SqlDbType.Float)
            {
                Direction = ParameterDirection.Input,
                Value = req.igst
            });
            param.Add(new SqlParameter("@IsActive", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Input,
                Value = req.isActive
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spCustomer_Insert_Update");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet GetCustomer(int? custId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = custId
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spCustomer_GetAll_ById");
        }

        public int DeleteCustomer(int custId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@CustomerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = custId
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spCustomer_Delete");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }
    }
}
