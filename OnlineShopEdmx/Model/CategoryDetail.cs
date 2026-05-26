using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopEdmx.Model
{
    public class    CategoryDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name Requird")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        [RegularExpression (@"^[A-Za-z\s]+$", ErrorMessage = "Category name can only contain letters" )]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
