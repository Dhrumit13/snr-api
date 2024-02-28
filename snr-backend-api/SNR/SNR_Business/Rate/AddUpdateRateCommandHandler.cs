using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Rate
{
    public class AddUpdateRateCommand : ICommandWithResponse<AddUpdateRateCommandResult>
    {
        public int? rateId { get; set; }
        public int? customerId { get; set; }
        public string transportationMode { get; set; }
        public string city { get; set; }
        public string minWeight { get; set; }
        public string ratePerKg { get; set; }
        public string ratePerPiece { get; set; }
    }
    public class AddUpdateRateCommandResult
    {
        public int resFlag { get; set; }
    }
    public class AddUpdateRateCommandHandler : ICommandHandler<AddUpdateRateCommand, AddUpdateRateCommandResult>
    {
        private readonly IdRate _rate;
        public AddUpdateRateCommandHandler(IdRate Rate)
        {
            _rate = Rate;
        }
        public AddUpdateRateCommandResult Handle(AddUpdateRateCommand cmd)
        {
            var _resFlag = _rate.AddUpdateRate(
                 new RateEntity
                 {
                     rateId = cmd.rateId,
                     customerId = cmd.customerId,
                     transportationMode = cmd.transportationMode,
                     city = cmd.city,
                     minWeight = cmd.minWeight,
                     ratePerKg = cmd.ratePerKg,
                     ratePerPiece = cmd.ratePerPiece
                 });
            return new AddUpdateRateCommandResult { resFlag = _resFlag };
        }
    }
}