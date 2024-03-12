using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Receiver
{
    public class DeleteReceiverCommand : ICommandWithResponse<DeleteReceiverCommandResult>
    {
        public int receiverId { get; set; }
    }
    public class DeleteReceiverCommandResult
    {
        public int resFlag { get; set; }
    }
    public class DeleteReceiverCommandHandler : ICommandHandler<DeleteReceiverCommand, DeleteReceiverCommandResult>
    {
        private readonly IdReceiver _Receiver;
        public DeleteReceiverCommandHandler(IdReceiver Receiver)
        {
            _Receiver = Receiver;
        }
        public DeleteReceiverCommandResult Handle(DeleteReceiverCommand cmd)
        {

            var _resFlag = _Receiver.DeleteReceiver(cmd.receiverId);
            return new DeleteReceiverCommandResult { resFlag = _resFlag };
        }
    }
}