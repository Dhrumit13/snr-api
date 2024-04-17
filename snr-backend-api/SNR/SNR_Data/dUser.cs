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
    public interface IdUser
    {
        int AddUpdateUser(UserEntity req);
        DataSet GetUser(int? userId);
        int DeleteUser(int userId);
        DataSet UserAuthentication(string userName, string password);
    }

    public class dUser : IdUser
    {
        private readonly ISQLHelper _helper;
        public dUser(ISQLHelper helper)
        {
            _helper = helper;
        }
        public int AddUpdateUser(UserEntity req)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = req.userId
            });
            param.Add(new SqlParameter("@UserName", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.userName
            });
            param.Add(new SqlParameter("@Password", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.password
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
            param.Add(new SqlParameter("@role", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.role
            });
            param.Add(new SqlParameter("@City", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = req.city
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spUser_Insert_Update");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet GetUser(int? userId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = userId
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spUser_GetAll_ById");
        }

        public int DeleteUser(int userId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Flag", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            });
            param.Add(new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = userId
            });
            SqlCommand cmd = _helper.CreateCommandObject(param.ToArray(), "spUser_Delete");
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@Flag"].Value.ToString().ToInt();
        }

        public DataSet UserAuthentication(string userName, string password)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@userName", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = userName
            });
            param.Add(new SqlParameter("@password", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = password
            });
            return _helper.ReturnWithDataSet(param.ToArray(), "spUser_Auth");
        }
    }
}
