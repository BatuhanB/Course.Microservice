using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Course.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public List<Address> Addresses { get; set; }
        public List<CardInformation> CardInformations { get; set; }
    }
}
