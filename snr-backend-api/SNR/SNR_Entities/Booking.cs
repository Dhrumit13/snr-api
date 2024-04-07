namespace SNR_Entities
{
    public class BookingEntity
    {
        public long? bookingId { get; set; }
        public int? customerIdf { get; set; }
        public int? receiverIdf { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public decimal? weight { get; set; }
        public int? quantity { get; set; }
        public decimal? grossAmount { get; set; }
        public string packageType { get; set; }
        public string paymentMode { get; set; }
        public decimal? netAmount { get; set; }
        public string remarks { get; set; }
        public DateTime? bookingDate { get; set; }
        public List<BookingChargestbl> otherCharges { get; set; }
        public CustomerEntity customer { get; set; }
        public ReceiverEntity receiver { get; set; }
    }


    public class BookingChargestbl
    {
        public long? bookingId { get; set; }
        public int otherChargeId { get; set; }
        public decimal value { get; set; }
        public ChargeType chargeType { get; set; }

    }

    public class ChargeType
    {
        public int otherChargeId { get; set; }
        public string otherChargeName { get; set; }
        public decimal amount { get; set; }
    }
}