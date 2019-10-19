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
    public class Category : BaseModel
    {
        public Category()
        {
            Ecards = new HashSet<Ecard>();
        }

        [Key]
        public int CategoryID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(100, ErrorMessage = "Đường link category không được vượt quá {1} ký tự!")]
        [DisplayName("Đường link category")]
        public string CategorySlug { get; set; }

        [StringLength(100, ErrorMessage = "Tên phân loại không được vượt quá {1} ký tự!")]
        [DisplayName("Tên phân loại")]
        public string CategoryName { get; set; }

        public virtual ICollection<Ecard> Ecards { get; set; }
    }
}
