using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.OtherCharges
{
    public class AddUpdateOtherChargesCommand : ICommandWithResponse<AddUpdateOtherChargesCommandResult>
    {
        public int? OtherChargeId { get; set; }
        public string OtherChargeName { get; set; }
        public int? amount { get; set; }
    }
    public class AddUpdateOtherChargesCommandResult
    {
        public int resFlag { get; set; }
    }
    public class AddUpdateOtherChargesCommandHandler : ICommandHandler<AddUpdateOtherChargesCommand, AddUpdateOtherChargesCommandResult>
    {
        private readonly IdOtherCharges _OtherCharges;
        public AddUpdateOtherChargesCommandHandler(IdOtherCharges OtherCharges)
        {
            _OtherCharges = OtherCharges;
        }
        public AddUpdateOtherChargesCommandResult Handle(AddUpdateOtherChargesCommand cmd)
        {
            var _resFlag = _OtherCharges.AddUpdateOtherCharges(
                 new OtherChargesEntity
                 {
                     otherChargeId = cmd.OtherChargeId,
                     otherChargeName = cmd.OtherChargeName,
                     amount = cmd.amount
                 });
            return new AddUpdateOtherChargesCommandResult { resFlag = _resFlag };
        }
    }
}