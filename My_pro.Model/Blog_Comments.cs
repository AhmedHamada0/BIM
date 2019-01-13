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
    public class Blog_Comments
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Blogid{ get; set; }
        [Required]
        public string User_id_Co { get; set; }
        [Required]
        public string User_img { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
