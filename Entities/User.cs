using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AtlantisPortals.API.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        [MaxLength(200)]
        public string FullName { get; set; }
        public bool IsClient { get; set; }
        public int AgencyId { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
