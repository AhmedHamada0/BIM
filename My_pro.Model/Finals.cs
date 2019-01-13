using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Finals
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string UserId { set; get; }
        [Required]
        public int Marks { set; get; }
        [Required]
        public string Level_id { set; get; }

        public DateTime Date { set; get; }

        public ICollection<QExams_Finals> QExams_Finals { get; set; }

    }
}
