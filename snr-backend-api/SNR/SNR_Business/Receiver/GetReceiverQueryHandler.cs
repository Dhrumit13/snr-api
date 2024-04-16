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

namespace SNR_Business.Receiver
{
    public class GetReceiverQuery : IQuery<GetReceiverQueryResult>
    {
        public int? custId { get; set; }
    }
    public class GetReceiverQueryResult
    {
        public ReceiverEntity[] Receivers { get; set; }
    }
    public class GetRateQueryHandler : IQueryHandler<GetReceiverQuery, GetReceiverQueryResult>
    {
        private readonly IdReceiver _cust;
        public GetRateQueryHandler(IdReceiver cust)
        {
            _cust = cust;
        }

        public GetReceiverQueryResult Get(GetReceiverQuery query)
        {
            var res = new GetReceiverQueryResult();
            var ds = _cust.GetReceiver(query.custId);
            if (!ds.IsNullOrEmpty()) {
                List<ReceiverEntity> lstCust = new List<ReceiverEntity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstCust.Add(new ReceiverEntity
                    {
                        receiverId = dr["receiverId"].ObjToNullableInt32(),
                        receiverName = dr["receiverName"].ToString(),
                        email = dr["email"].ToString(),
                        mobile = dr["mobile"].ToString(),
                        gstNo = dr["gstNo"].ToString(),
                        address = dr["address"].ToString(),
                        city = dr["city"].ToString(),
                        isActive = dr["isActive"].ObjToNullableBool()
                    });
                }
                res.Receivers = lstCust.ToArray();
            }
            return res;
        }
    }
}
