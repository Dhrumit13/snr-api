using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNR_Business.Common.Handler;
using SNR_Business.Common.Util;
using SNR_Data;
using SNR_Entities;

namespace SNR_Business.Customer
{
    public class GetCustomerQuery : IQuery<GetCustomerQueryResult>
    {
        public int? custId { get; set; }
    }
    public class GetCustomerQueryResult
    {
        public CustomerEntity[] Customers { get; set; }
    }
    public class GetRateQueryHandler : IQueryHandler<GetCustomerQuery, GetCustomerQueryResult>
    {
        private readonly IdCustomer _cust;
        public GetRateQueryHandler(IdCustomer cust)
        {
            _cust = cust;
        }

        public GetCustomerQueryResult Get(GetCustomerQuery query)
        {
            var res = new GetCustomerQueryResult();
            var ds = _cust.GetCustomer(query.custId);
            if (!ds.IsNullOrEmpty()) {
                List<CustomerEntity> lstCust = new List<CustomerEntity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstCust.Add(new CustomerEntity
                    {
                        customerId = dr["customerId"].ObjToNullableInt32(),
                        name = dr["name"].ToString(),
                        email = dr["email"].ToString(),
                        mobile = dr["mobile"].ToString(),
                        gstNo = dr["gstNo"].ToString(),
                        address = dr["address"].ToString(),
                        city = dr["city"].ToString(),
                        state = dr["state"].ToString(),
                        cgst = dr["cgst"].ObjToNullableFloat(),
                        sgst = dr["sgst"].ObjToNullableFloat(),
                        igst = dr["igst"].ObjToNullableFloat(),
                        isActive = dr["isActive"].ObjToNullableBool()
                    });
                }
                res.Customers = lstCust.ToArray();
            }
            return res;
        }
    }
}
