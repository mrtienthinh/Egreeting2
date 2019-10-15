using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        [Range(1,12)]
        [DisplayName("Tháng tính phí")]
        public int Month { get; set; }

        [Required]
        [DisplayName("Năm tính phí")]
        public int Year { get; set; }

        [Required]
        [DisplayName("Trạng thái tính phí")]
        public bool PaymentStatus { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
