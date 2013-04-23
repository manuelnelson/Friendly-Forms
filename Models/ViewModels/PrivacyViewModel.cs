using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class PrivacyViewModel : IViewModel
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }

        [Required]
        [Display(Name = "Need Privacy")]
        public int NeedPrivacy { get; set; }

        [Display(Name = "Supervision Father")]
        public int NeedSupervision { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Supervision How")]
        public string SupervisionHow { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Supervision Where")]
        public string SupervisionWhere { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        [Display(Name = "Supervision Who")]
        public string SupervisionWho { get; set; }
        [Display(Name = "Supervision Costs")]
        public int? SupervisionCost { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Privacy>();
        }
    }
}
