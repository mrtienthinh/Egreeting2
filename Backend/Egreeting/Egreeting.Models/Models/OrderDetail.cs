using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class OrderDetail : BaseModel
    {
        [Key]
        public int OrderDetailID { get; set; }

        [DisplayName("Hẹn giờ gửi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:R}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleTime { get; set; }

        [Required]
        [DisplayName("Trạng thái thư")]
        public bool SendStatus { get; set; }

        public virtual Ecard Ecard { get; set; }

        public virtual Order Order { get; set; }


    }
}
