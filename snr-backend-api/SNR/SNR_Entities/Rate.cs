namespace SNR_Entities
{
    public class RateEntity
    {
        public int? rateId { get; set; }
        public int? customerId { get; set; }
        public string transportationMode { get; set; }
        public string city { get; set; }
        public string minWeight { get; set; }
        public string ratePerKg { get; set; }
        public string ratePerPiece { get; set; }
    }
}