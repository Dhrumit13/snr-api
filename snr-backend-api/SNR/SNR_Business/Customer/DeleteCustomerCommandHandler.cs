using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Customer
{
    public class DeleteCustomerCommand : ICommandWithResponse<DeleteCustomerCommandResult>
    {
        public int custId { get; set; }
    }
    public class DeleteCustomerCommandResult
    {
        public int resFlag { get; set; }
    }
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, DeleteCustomerCommandResult>
    {
        private readonly IdCustomer _Customer;
        public DeleteCustomerCommandHandler(IdCustomer Customer)
        {
            _Customer = Customer;
        }
        public DeleteCustomerCommandResult Handle(DeleteCustomerCommand cmd)
        {

            var _resFlag = _Customer.DeleteCustomer(cmd.custId);
            return new DeleteCustomerCommandResult { resFlag = _resFlag };
        }
    }
}