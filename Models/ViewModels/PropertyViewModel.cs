using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class PropertyViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }

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
            return new Property()
                {
                    DividingProperty = DividingProperty,
                    PersonalProperty = PersonalProperty,
                    RealEstate = RealEstate,
                    RealEstateDescription = RealEstateDescription,
                    UserId = UserId
                };
        }
    }
}
