using LogA.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogA.Models.Profile
{
    public class ProfileModel
    {
        
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        

        public virtual ApplicationUser User { get; set; }
    }
}
