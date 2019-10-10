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

        [Required]
        public EgreetingUser User { get; set; }


    }
}
