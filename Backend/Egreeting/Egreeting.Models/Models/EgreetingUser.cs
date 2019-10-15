using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class EgreetingUser : BaseModel
    {
        public EgreetingUser()
        {
            Ecards = new HashSet<Ecard>();
            Feedbacks = new HashSet<Feedback>();
            ScheduleSenders = new HashSet<ScheduleSender>();
            EgreetingRoles = new HashSet<EgreetingRole>();
        }

        [Key]
        public int EgreetingUserID { get; set; }

        [DisplayName("Tên đường link tùy biến")]
        [Index(IsUnique = true)]
        [StringLength(100, ErrorMessage = "Tên đường link tùy biến không được quá {1} ký tự!")]
        public string EgreetingUserSlug { get; set; }

        [DisplayName("Tên")]
        [StringLength(50, ErrorMessage = "Tên không được quá {1} ký tự!")]
        public string FirstName { get; set; }

        [DisplayName("Email")]
        [StringLength(100, ErrorMessage = "Email không được quá {1} ký tự!")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [NotMapped]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [DisplayName("Họ")]
        [StringLength(100, ErrorMessage = "Họ không được quá {1} ký tự!")]
        public string LastName { get; set; }

        [DisplayName("Ảnh đại diện")]
        public byte[] Avatar { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { get; set; }

        [StringLength(12, ErrorMessage = "Tên không được quá {1} ký tự!")]
        [MinLength(12)]
        public string CreditCardNumber { get; set; }

        [StringLength(3, ErrorMessage = "CVG chỉ bao gồm {1} ký tự!")]
        [MinLength(3)]
        [MaxLength(3)]
        public string CreditCardCVG { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDueDate { get; set; }

        public virtual ICollection<EgreetingRole> EgreetingRoles { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Ecard> Ecards { get; set; }
        public virtual Subcriber Subcriber { get; set; }
        public virtual ICollection<ScheduleSender> ScheduleSenders { get; set; }
    }
}
