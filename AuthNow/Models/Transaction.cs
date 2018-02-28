using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthNow.Models
{
    public class Transaction
    {
        public int TransactionId{ get; set; }
        public decimal Amount { get; set; }
        [Display(Name = "Campaign")]
        public int CampaignId { get; set; }
        [Display(Name = "Donor")]
        public int DonorId { get; set; }
        public string PaymentOption { get; set; }
        public DateTime DateCreated { get; set; }

        public Campaign Campaign { get; set; }
        public Donor Donor { get; set; }

    }
}