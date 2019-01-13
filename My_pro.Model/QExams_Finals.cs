using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class QExams_Finals
    {
        [Key, Column(Order = 0)]
        public string QExam_Id { get; set; }
        [Key, Column(Order = 1)]
        public string Final_Id { get; set; }

        [ForeignKey("QExam_Id")]
        public QExams QExam { get; set; }
        [ForeignKey("Final_Id")]
        public Finals Final { get; set; }
    }
}
