using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Rate
{
    public class DeleteOtherChargesCommand : ICommandWithResponse<DeleteOtherChargesCommandResult>
    {
        public int OtherChargeId { get; set; }
    }
    public class DeleteOtherChargesCommandResult
    {
        public int resFlag { get; set; }
    }
    public class DeleteOtherChargesCommandHandler : ICommandHandler<DeleteOtherChargesCommand, DeleteOtherChargesCommandResult>
    {
        private readonly IdOtherCharges _OtherCharges;
        public DeleteOtherChargesCommandHandler(IdOtherCharges OtherCharges)
        {
            _OtherCharges = OtherCharges;
        }
        public DeleteOtherChargesCommandResult Handle(DeleteOtherChargesCommand cmd)
        {

            var _resFlag = _OtherCharges.DeleteOtherCharges(cmd.OtherChargeId);
            return new DeleteOtherChargesCommandResult { resFlag = _resFlag };
        }
    }
}