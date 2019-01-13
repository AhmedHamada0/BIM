using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class Blogs
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Toptext { set; get; }
        [Required]
        public string Buttomtext { set; get; }
        [Required]
        public string UserId { set; get; }
        public string UserName { set; get; }
        [Required]
        public DateTime Date { set; get; }
        public string Link1 { set; get; }
        public string Link2 { set; get; }
        public string Link3 { set; get; }
        public string URL { set; get; }
        public bool img { set; get; }


    }
}
