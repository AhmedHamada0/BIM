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
    public class Levels_Videos
    {
        [Key, Column(Order = 0)]
        public string Level_Id { get; set; }
        [Key, Column(Order = 1)]
        public string Video_Id { get; set; }

        [ForeignKey("Video_Id")]
        public Videos Video { get; set; }
        [ForeignKey("Level_Id")]
        public Levels Level { get; set; }
    }
}
