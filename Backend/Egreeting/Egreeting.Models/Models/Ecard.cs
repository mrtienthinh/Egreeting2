using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egreeting.Models.Models
{
    public class Ecard : BaseModel
    {
        public Ecard()
        {
            Categories = new HashSet<Category>();
        }

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
        [StringLength(150, ErrorMessage = "Đường dẫn lưu thiệp không được quá {1} ký tự!")]
        [DisplayName("Ecard's link")]
        public string EcardUrl { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Thumbnail thiệp không được quá {1} ký tự!")]
        [DisplayName("Thumbnail")]
        public string ThumbnailUrl { get; set; }

        [Required]
        [DisplayName("Ecard's price")]
        public double Price { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
