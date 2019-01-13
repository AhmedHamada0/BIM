using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace My_pro.Model.Entites
{
    public class Users : IdentityUser
    {

        public string Picture { get; set; }
        public int XP { get; set; }
        public string Facebook_Link { get; set; }
        public string Linkedin_Link { get; set; }
        public string Link { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Friends> Friends { get; set; }
        public ICollection<Users_Levels> Users_Levels { get; set; }
        public ICollection<Users_Finals> Users_Finals { get; set; }
        public ICollection<Users_Chapters> Users_Chapters { get; set; }
        public ICollection<Users_Diplomacd> Users_Diploma { get; set; }

    }
}