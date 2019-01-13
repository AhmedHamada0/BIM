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
    public class Users_Finals
    {

        [Key]
        public string Id { get; set; }
        [Required]
        public string User_Id { get; set; }
        [Required]
        public string Level_Id { get; set; }

        public int Marks { get; set; }

        [Required]
        public DateTime Date { set; get; }
    }
}
