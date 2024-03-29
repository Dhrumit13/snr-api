﻿using SNR_Business.Common.Handler;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Customer
{
    public class AddUpdateCustomerCommand : ICommandWithResponse<AddUpdateCustomerCommandResult>
    {
        public int? customerId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string gstNo { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public float? cgst { get; set; }
        public float? sgst { get; set; }
        public float? igst { get; set; }
        public bool? isActive { get; set; }
    }
    public class AddUpdateCustomerCommandResult
    {
        public int resFlag { get; set; }
    }
    public class AddUpdateReceiverCommandHandler : ICommandHandler<AddUpdateCustomerCommand, AddUpdateCustomerCommandResult>
    {
        private readonly IdCustomer _Customer;
        public AddUpdateReceiverCommandHandler(IdCustomer Customer)
        {
            _Customer = Customer;
        }
        public AddUpdateCustomerCommandResult Handle(AddUpdateCustomerCommand cmd)
        {

            var _resFlag = _Customer.AddUpdateCustomer(
                 new CustomerEntity
                 {
                     customerId = cmd.customerId,
                     name = cmd.name,
                     email = cmd.email,
                     mobile = cmd.mobile,
                     gstNo = cmd.gstNo, 
                     address = cmd.address,
                     city = cmd.city,
                     state = cmd.state,
                     cgst = cmd.cgst,
                     sgst = cmd.sgst,
                     igst = cmd.igst,
                     isActive = cmd.isActive
                 }
                 );
            return new AddUpdateCustomerCommandResult { resFlag = _resFlag };
        }
    }
}