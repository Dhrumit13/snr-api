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

namespace SNR_Business.User
{
    public class GetUserQuery : IQuery<GetUserQueryResult>
    {
        public int? UserId { get; set; }
    }
    public class GetUserQueryResult
    {
        public UserEntity[] users { get; set; }
    }
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly IdUser _user;
        public GetUserQueryHandler(IdUser User)
        {
            _user = User;
        }

        public GetUserQueryResult Get(GetUserQuery query)
        {
            var res = new GetUserQueryResult();
            var ds = _user.GetUser(query.UserId);
            if (!ds.IsNullOrEmpty()) {
                List<UserEntity> lstUser = new List<UserEntity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstUser.Add(new UserEntity
                    {
                        userId = dr["userId"].ObjToNullableInt32(),
                        userName = dr["userName"].ToString(),
                        password = dr["password"].ToString(),
                        email = dr["email"].ToString(),
                        mobile = dr["mobile"].ToString(),
                        role = dr["role"].ToString(),
                    });
                }
                res.users = lstUser.ToArray();
            }
            return res;
        }
    }
}
