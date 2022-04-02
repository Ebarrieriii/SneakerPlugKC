using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SneakerPlugKC.DATA.EF
{

    public class SizeMetaData
    {
        public int SizeId { get; set; }

        [Required]
        [Range(4, 16, ErrorMessage = "Shoe size must be between 4 & 16.")]
        [Display(Name = "Size")]
        public double Sizes { get; set; }
    }

    [MetadataType(typeof(SizeMetaData))]
    public partial class Size
    {

    }

    public class ShoeMetaData
    {
        public int ShoeID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Shoe Name should be at least 2 letters and can't be more than 60.")]
        [Display(Name = "Name")]
        public string ShoeName { get; set; }
        public int SizeId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(200, ErrorMessage = "This field must be under 200 characters.")]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100.")]
        public int Quantity { get; set; }

        public bool IsInStock { get; set; }

        [Display(Name = "Photo")]
        public string ShoePhoto { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "This field must be under 20 characters.")]
        public string Condition { get; set; }
    }

    [MetadataType(typeof(ShoeMetaData))]
    public partial class Shoe
    {

    }
}
