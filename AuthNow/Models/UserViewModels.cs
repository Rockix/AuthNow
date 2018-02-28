using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthNow.Models
{
    public class UserViewModels
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Campaign> Campaigns { get; set; }
    }
}