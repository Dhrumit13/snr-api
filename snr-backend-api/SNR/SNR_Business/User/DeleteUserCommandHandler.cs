using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.User
{
    public class DeleteUserCommand : ICommandWithResponse<DeleteUserCommandResult>
    {
        public int userId { get; set; }
    }
    public class DeleteUserCommandResult
    {
        public int resFlag { get; set; }
    }
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, DeleteUserCommandResult>
    {
        private readonly IdUser _user;
        public DeleteUserCommandHandler(IdUser User)
        {
            _user = User;
        }
        public DeleteUserCommandResult Handle(DeleteUserCommand cmd)
        {

            var _resFlag = _user.DeleteUser(cmd.userId);
            return new DeleteUserCommandResult { resFlag = _resFlag };
        }
    }
}