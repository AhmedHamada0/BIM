using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_pro.Model
{
    public class UploadFile
    {
        [Key]
        public string Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string User_Id { set; get; }
        [Required]
        public string URL { set; get; }
        [Required]
        public string Img { set; get; }
        [Required]
        public DateTime Time { set; get; }
        [Required]
        public string Type { set; get; }
        [Required]
        public int Size { set; get; }

    }
}
