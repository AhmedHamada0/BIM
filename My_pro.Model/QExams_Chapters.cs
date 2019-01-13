using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class QExams_Chapters //<---Delete--->//
    {
        [Key, Column(Order = 0)]
        public string QExam_Id { get; set; }
        [Key, Column(Order = 1)]
        public string Chapter_Id { get; set; }

        [ForeignKey("QExam_Id")]
        public QExams QExam { get; set; }
        [ForeignKey("Chapter_Id")]
        public Chapters Chapter { get; set; }
    }
}
