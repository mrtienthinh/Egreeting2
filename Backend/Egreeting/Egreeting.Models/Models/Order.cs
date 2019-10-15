using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Order : BaseModel
    {
        [Key]
        public int OrderID { get; set; }

        [StringLength(100, ErrorMessage = "Tên người gửi không được vượt quá {1} ký tự!")]
        [DisplayName("Tên người gửi")]
        public string SenderName { get; set; }

        [EmailAddress]
        [Required]
        [DisplayName("Địa chỉ email người nhận")]
        [StringLength(100, ErrorMessage = "Email người nhận không được vượt quá {1} ký tự!")]

        public string RecipientEmail { get; set; }

        [StringLength(100, ErrorMessage = "Chủ đề thiệp không được vượt quá {1} ký tự!")]
        [Required]
        [DisplayName("Chủ đề lời chúc")]
        public string SendSubject { get; set; }

        [DisplayName("Lời chúc")]
        [StringLength(500, ErrorMessage = "Nội dung lời chúc không được vượt quá {1} ký tự!")]
        public string SendMessage { get; set; }

        [Required]
        [DisplayName("Trạng thái gửi")]
        public bool SendStatus { get; set; }

        public double TotalPrice { get; set; }

        public virtual EgreetingUser User { get; set; }
    }
}
