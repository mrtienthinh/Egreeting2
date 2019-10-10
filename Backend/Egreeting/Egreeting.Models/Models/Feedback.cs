using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Feedback : BaseModel
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required]
        [DisplayName("Chủ đề phản hồi")]
        [StringLength(200, ErrorMessage = "Chủ đề không được quá {1} ký tự!")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Nội dung phản hồi")]
        [StringLength(500, ErrorMessage = "Nội dung không được quá {1} ký tự!")]
        public string Content { get; set; }

        public virtual Ecard Ecard { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
