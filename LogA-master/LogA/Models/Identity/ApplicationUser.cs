using LogA.Models.Profile;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogA.Models.Identity
{
    public class ApplicationUser: IdentityUser
    {
        [ForeignKey("ProfileModel")]
        public int ProfileId { get; set; }
        public virtual ProfileModel Profile { get; set; }
    }
}
