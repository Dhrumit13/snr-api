﻿namespace SNR_Entities
{
    public class CustomerEntity
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
}