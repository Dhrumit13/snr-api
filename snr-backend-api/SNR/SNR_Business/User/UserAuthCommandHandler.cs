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
                var userId = ds.Tables[0].Rows[0]["userId"].ObjToInt32();
                if (userId > 0) {
                    return new UserAuthCommandResult { resFlag =userId };
                }
            }
            return new UserAuthCommandResult { resFlag = 0 };
        }
    }
}