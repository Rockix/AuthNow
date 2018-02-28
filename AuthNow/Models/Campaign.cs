using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthNow.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Target { get; set; }
        public decimal? CurrentAmount { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Category")]
        public  int CategoryId { get; set; }
        public int UserId { get; set; }

        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
}