using SNR_Business.Common.Handler;
using SNR_Business.Common.Util;
using SNR_Data;
using SNR_Entities;
using System.Xml;

namespace SNR_Business.User
{
    public class AddUpdateUserCommand : ICommandWithResponse<AddUpdateUserCommandResult>
    {
        public int? userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string role { get; set; }
        public string city { get; set; }
    }
    public class AddUpdateUserCommandResult
    {
        public int resFlag { get; set; }
    }
    public class AddUpdateUserCommandHandler : ICommandHandler<AddUpdateUserCommand, AddUpdateUserCommandResult>
    {
        private readonly IdUser _user;
        public AddUpdateUserCommandHandler(IdUser User)
        {
            _user = User;
        }
        public AddUpdateUserCommandResult Handle(AddUpdateUserCommand cmd)
        {
            cmd.password = EncryptDecrypt.Encrypt(cmd.password.Trim(), true);
            var _resFlag = _user.AddUpdateUser(
                 new UserEntity
                 {
                     userId = cmd.userId,
                     userName = cmd.userName,
                     password = cmd.password,
                     email = cmd.email,
                     mobile = cmd.mobile,
                     role = cmd.role,
                     city = cmd.city,
                 });
            return new AddUpdateUserCommandResult { resFlag = _resFlag };
        }
    }
}