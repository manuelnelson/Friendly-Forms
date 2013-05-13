using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class PropertyViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }

        [Required]
        [Display(Name = "Real Estate")]
        public int RealEstate { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Details")]
        public string RealEstateDescription { get; set; }

        [Required]
        [Display(Name = "Personal property")]
        public int PersonalProperty { get; set; }

        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Dividing property")]
        public string DividingProperty { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Property>();
        }
    }
}
