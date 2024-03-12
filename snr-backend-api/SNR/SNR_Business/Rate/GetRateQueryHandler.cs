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

namespace SNR_Business.Rate
{
    public class GetRateQuery : IQuery<GetRateQueryResult>
    {
        public int? rateId { get; set; }
        public int? customerId { get; set; }
    }
    public class GetRateQueryResult
    {
        public RateEntity[] Rates { get; set; }
    }
    public class GetRateQueryHandler : IQueryHandler<GetRateQuery, GetRateQueryResult>
    {
        private readonly IdRate _rate;
        public GetRateQueryHandler(IdRate rate)
        {
            _rate = rate;
        }

        public GetRateQueryResult Get(GetRateQuery query)
        {
            var res = new GetRateQueryResult();
            var ds = _rate.GetRate(query.rateId, query.customerId);
            if (!ds.IsNullOrEmpty()) {
                List<RateEntity> lstrate = new List<RateEntity>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstrate.Add(new RateEntity
                    {
                        rateId = dr["rateId"].ObjToNullableInt32(),
                        customerId = dr["customerId"].ObjToNullableInt32(),
                        transportationMode = dr["transportationMode"].ToString(),
                        city = dr["city"].ToString(),
                        minWeight = dr["minWeight"].ToString(),
                        ratePerKg = dr["ratePerKg"].ToString(),
                        ratePerPiece = dr["ratePerPiece"].ToString()                       
                    });
                }
                res.Rates = lstrate.ToArray();
            }
            return res;
        }
    }
}
