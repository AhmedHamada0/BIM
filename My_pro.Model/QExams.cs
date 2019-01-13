using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class QExams
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string LevelId { set; get; }
        [Required]
        public string LevelName { set; get; }
        [Required]
        public string Question { set; get; }
        [Required]
        public string S1 { set; get; }
        [Required]
        public string S2 { set; get; }
        [Required]
        public string S3 { set; get; }
        [Required]
        public string S4 { set; get; }
        [Required]
        public string S { set; get; }
        
        public bool Final { set; get; }
        public bool Chapter { set; get; }
        public bool MoreQ { set; get; }

        public int NumChapter { set; get; }


        public ICollection<QExams_Chapters> QExams_Chapters { get; set; }
        public ICollection<QExams_Finals> QExams_Finals { get; set; }


    }
}
