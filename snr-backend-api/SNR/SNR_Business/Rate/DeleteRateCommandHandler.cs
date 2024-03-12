using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Rate
{
    public class DeleteRateCommand : ICommandWithResponse<DeleteRateCommandResult>
    {
        public int rateId { get; set; }
    }
    public class DeleteRateCommandResult
    {
        public int resFlag { get; set; }
    }
    public class DeleteRateCommandHandler : ICommandHandler<DeleteRateCommand, DeleteRateCommandResult>
    {
        private readonly IdRate _rate;
        public DeleteRateCommandHandler(IdRate Rate)
        {
            _rate = Rate;
        }
        public DeleteRateCommandResult Handle(DeleteRateCommand cmd)
        {

            var _resFlag = _rate.DeleteRate(cmd.rateId);
            return new DeleteRateCommandResult { resFlag = _resFlag };
        }
    }
}