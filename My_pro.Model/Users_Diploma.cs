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
    public class Users_Diplomacd
    {
        [Key, Column(Order = 0)]
        public string User_Id { get; set; }
        [Key, Column(Order = 1)]
        public string Diploma_Id { get; set; }


        [ForeignKey("Diploma_Id")]
        public Diploma Diploma { get; set; }
        [ForeignKey("User_Id")]
        public Users User { get; set; }
    }
}
