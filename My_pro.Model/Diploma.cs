using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Diploma
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Level { set; get; }
        [Required]
        public string UserId { set; get; }
        [Required]
        public string UserName { set; get; }
        [Required]
        public int Marks { set; get; }
        [Required]
        public DateTime Date { set; get; }

        public ICollection<Users_Diplomacd> Users_Diploma { get; set; }


    }
}
