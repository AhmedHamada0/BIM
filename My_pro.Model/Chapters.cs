using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Chapters
    {
        [Key]
        public string Id { set; get; }
        public string Name { set; get; }
        [Required]
        public DateTime Date { set; get; }
        [Required]
        public string Level_id { set; get; }
        [Required]
        public string Level_Name { set; get; }
        [Required]
        public int Count { set; get; }


        public ICollection<Users_Chapters> Users_Chapters { get; set; }
        public ICollection<QExams_Chapters> QExams_Chapters { get; set; }
    }
}
