using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class ScheduleSender : BaseModel
    {

        [Key]
        public int ScheduleSenderID { get; set; }

        [DisplayName("Hẹn giờ gửi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:R}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleTime { get; set; }

        // Enum ScheduleType 0: Once, 1: Daily, 2: Monthly, 3: Annual
        [DisplayName("Loại lịch trình")]
        [Range(0,3, ErrorMessage = "Loại lịch trình không chính xác!")]
        public int ScheduleType { get; set; }

        [StringLength(100, ErrorMessage = "Tên người gửi không được quá {1} ký tự!")]
        [DisplayName("Tên người gửi")]
        public string SenderName { get; set; }

        [EmailAddress]
        [DisplayName("Địa chỉ email người nhận")]
        [StringLength(100, ErrorMessage = "Email người nhận không được vượt quá {1} ký tự!")]
        public string RecipientEmail { get; set; }

        [StringLength(100, ErrorMessage = "Chủ đề thiệp không được quá {1} ký tự!")]
        [DisplayName("Chủ đề lời chúc")]
        public string SendSubject { get; set; }

        [DisplayName("Lời chúc")]
        [StringLength(500, ErrorMessage = "Nội dung lời chúc không được quá {1} ký tự!")]
        public string SendMessage { get; set; }

        public virtual Ecard Ecard { get; set; }

        public virtual EgreetingUser User { get; set; }
    }
}
