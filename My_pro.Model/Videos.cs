using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Videos
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Lectrue_Name { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string Level_id { get; set; }
        [Required]
        public string Level_Name { get; set; }
        [Required]
        public DateTime Date { set; get; }
        [Required]
        public int Count { set; get; }

        public ICollection<Levels_Videos> Levels_Videos { get; set; }
    }
}
