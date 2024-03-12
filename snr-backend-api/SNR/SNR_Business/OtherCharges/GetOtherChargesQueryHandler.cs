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

namespace SNR_Business.OtherCharges
{
    public class GetOtherChargesQuery : IQuery<GetOtherChargesQueryResult>
    {
        public int? OtherChargesId { get; set; }
    }
    public class GetOtherChargesQueryResult
    {
        public OtherChargesEntity[] OtherChargess { get; set; }
    }
    public class GetUserQueryHandler : IQueryHandler<GetOtherChargesQuery, GetOtherChargesQueryResult>
    {
        private readonly IdOtherCharges _OtherCharges;
        public GetUserQueryHandler(IdOtherCharges OtherCharges)
        {
            _OtherCharges = OtherCharges;
        }

        public GetOtherChargesQueryResult Get(GetOtherChargesQuery query)
        {
            var res = new GetOtherChargesQueryResult();
            var ds = _OtherCharges.GetOtherCharges(query.OtherChargesId);
            if (!ds.IsNullOrEmpty()) {
                List<OtherChargesEntity> lstOtherCharges = new List<OtherChargesEntity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstOtherCharges.Add(new OtherChargesEntity
                    {
                        otherChargeId = dr["otherChargeId"].ObjToNullableInt32(),
                        otherChargeName = dr["otherChargeName"].ToString(),
                        amount = dr["amount"].ObjToNullableInt32()
                    });
                }
                res.OtherChargess = lstOtherCharges.ToArray();
            }
            return res;
        }
    }
}
