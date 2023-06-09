using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolManagement_331.Models.CustomModels
{
   public class ImageCustomeModel
    {
        
        public int ImageId { get; set; }
        [Required]
        public string ImageTitle { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int UserId { get; set; }
        public string User { get; set; }
        public HttpPostedFileBase IMAGEPATH { get; set; }

    }
}
