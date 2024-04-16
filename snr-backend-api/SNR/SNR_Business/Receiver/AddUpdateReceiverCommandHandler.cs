using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Receiver
{
    public class AddUpdateReceiverCommand : ICommandWithResponse<AddUpdateReceiverCommandResult>
    {
        public int? receiverId { get; set; }
        public string receiverName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string gstNo { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public bool? isActive { get; set; }
    }
    public class AddUpdateReceiverCommandResult
    {
        public int resFlag { get; set; }
    }
    public class AddUpdateReceiverCommandHandler : ICommandHandler<AddUpdateReceiverCommand, AddUpdateReceiverCommandResult>
    {
        private readonly IdReceiver _Receiver;
        public AddUpdateReceiverCommandHandler(IdReceiver Receiver)
        {
            _Receiver = Receiver;
        }
        public AddUpdateReceiverCommandResult Handle(AddUpdateReceiverCommand cmd)
        {

            var _resFlag = _Receiver.AddUpdateReceiver(
                 new ReceiverEntity
                 {
                     receiverId = cmd.receiverId,
                     receiverName = cmd.receiverName,
                     email = cmd.email,
                     mobile = cmd.mobile,
                     gstNo = cmd.gstNo,
                     address = cmd.address,
                     city = cmd.city,
                     isActive = cmd.isActive
                 }
                 );
            return new AddUpdateReceiverCommandResult { resFlag = _resFlag };
        }
    }
}