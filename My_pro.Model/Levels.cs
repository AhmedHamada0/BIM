using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Levels
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Pic { set; get; }

        public DateTime Date { set; get; }
        [Required]
        public string About { set; get; }
        [Required]
        public bool IsFree { set; get; }
        public ICollection<Users_Levels> Users_Levels { get; set; }
        public ICollection<Users_Finals> Users_Finals { get; set; }

        public ICollection<Levels_Videos> Levels_Videos { get; set; }

    }
}
