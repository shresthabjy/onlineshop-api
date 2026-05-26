using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Models
{
    public class ProductFeatureModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductFeatureId { get; set; }
        [Required(ErrorMessage = "Category Name Requird")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string ProductFeatureName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
}
