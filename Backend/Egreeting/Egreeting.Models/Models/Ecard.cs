using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egreeting.Models.Models
{
    public class Ecard : BaseModel
    {
        [Key]
        public int EcardID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(200, ErrorMessage = "Đường link của thiệp không được quá {1} ký tự!")]
        [DisplayName("Đường link của thiệp")]
        public string EcardSlug { get; set; }

        //Enum EcardType
        [Range(1, 3, ErrorMessage = "Loại thiệp không chính xác!")]
        [Required]
        [DisplayName("Loại thiệp")]
        public int EcardType { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Link rút gọn của thiệp không được quá {1} ký tự!")]
        [DisplayName("Đường link của thiệp")]
        public string EcardUrl { get; set; }

        public virtual Category Category { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
