using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthNow.Models
{
    public class Donor
    {
        public int DonorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfDonations { get; set; }
    }
}