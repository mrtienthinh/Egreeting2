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
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int OrderID { get; set; }

        [StringLength(100, ErrorMessage = "Tên người gửi không được vượt quá {1} ký tự!")]
        [DisplayName("Sender's Name")]
        public string SenderName { get; set; }

        [EmailAddress]
        [DisplayName("Recipient's Email")]
        [StringLength(100, ErrorMessage = "Email người nhận không được vượt quá {1} ký tự!")]

        public string RecipientEmail { get; set; }

        [StringLength(100, ErrorMessage = "Chủ đề thiệp không được vượt quá {1} ký tự!")]
        [DisplayName("Subject")]
        public string SendSubject { get; set; }

        [DisplayName("Message")]
        [StringLength(500, ErrorMessage = "Nội dung lời chúc không được vượt quá {1} ký tự!")]
        public string SendMessage { get; set; }

        [DisplayName("Schedule Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:R}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleTime { get; set; }

        [DisplayName("Sending Status")]
        public bool SendStatus { get; set; }

        public double TotalPrice { get; set; }

        public virtual EgreetingUser User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
