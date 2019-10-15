using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Subcriber : BaseModel
    {
        [Key]
        public int SubcriberID { get; set; }

        [EmailAddress]
        [Display(Name ="Subcriber's email")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá {1} ký tự!")]
        public string Email { get; set; }

        public EgreetingUser EgreetingUser { get; set; }


    }
}
