using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model.Entites
{
    public class Friends
    {
        [Key , Column(Order = 0)]
        public string  UserID1 { get; set; }

        [Key, Column(Order = 1)]
        public string UserID2 { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public bool Accept { get; set; }

        [ForeignKey("UserID1")]
        public Users user1 { get; set; }

        [ForeignKey("UserID2")]
        public Users user2 { get; set; }


    }
}
