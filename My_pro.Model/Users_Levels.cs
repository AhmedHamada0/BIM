using My_pro.Model.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Users_Levels
    {
        [Key, Column(Order = 0)]
        public string User_Id { get; set; }
        [Key, Column(Order = 1)]
        public string Level_Id { get; set; }

        public DateTime Time { get; set; }
        public int Marks { get; set; }

        [ForeignKey("Level_Id")]
        public Levels Levels { get; set; }
        [ForeignKey("User_Id")]
        public Users User { get; set; }

    }
}
