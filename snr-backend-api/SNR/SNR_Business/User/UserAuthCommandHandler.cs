using SNR_Business.Common.Handler;
using SNR_Business.Common.Util;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.User
{
    public class UserAuthCommand : ICommandWithResponse<UserAuthCommandResult>
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class UserAuthCommandResult
    {
        public int resFlag { get; set; }
        public string accessToken { get; set; }
        public LoggedInUserInfo userInfo { get; set; }
    }
    public class LoggedInUserInfo 
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string role { get; set; }
    }
    public class UserAuthCommandHandler : ICommandHandler<UserAuthCommand, UserAuthCommandResult>
    {
        private readonly IdUser _user;
        public UserAuthCommandHandler(IdUser User)
        {
            _user = User;
        }
        public UserAuthCommandResult Handle(UserAuthCommand cmd)
        {
            cmd.password = EncryptDecrypt.Encrypt(cmd.password.Trim(), true);
            var ds = _user.UserAuthentication(cmd.userName,cmd.password);
            if (!ds.IsNullOrEmpty()) {
                var userInfo = new LoggedInUserInfo();
                userInfo.userId = ds.Tables[0].Rows[0]["userId"].ObjToInt32();
                userInfo.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                userInfo.email = ds.Tables[0].Rows[0]["email"].ToString();
                userInfo.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                userInfo.role = ds.Tables[0].Rows[0]["role"].ToString();

                if (userInfo.userId > 0) {
                    var token = Jwt.GetJwtToken(new TokenInfo { 
                    userId = userInfo.userId.ToString(),
                    userName = "",
                    tokenExpireInMin = 3600,
                    });

                    return new UserAuthCommandResult { resFlag = userInfo.userId, 
                        userInfo= userInfo,
                        accessToken = token };
                }
            }
            return new UserAuthCommandResult { resFlag = 0 };
        }
    }
}