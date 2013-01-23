using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class OtherChildrenViewModel : IViewModel
    {
        public long Id { get; set; }
        public bool IsOtherParent { get; set; }
        [Required]
        public int UserId { get; set; }
        [Display(Name = "Legally Responsible")]
        public int? LegallyResponsible { get; set; }
        [Display(Name = "At home")]
        public int? AtHome { get; set; }
        public int? Support { get; set; }
        public int? Preexisting { get; set; }
        [Display(Name = "In court")]
        public int? InCourt { get; set; }
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string Details { get; set; }

        public OtherChildViewModel OtherChildViewModel { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new OtherChildren()
                {
                    IsOtherParent = IsOtherParent,
                    AtHome = AtHome ?? 0,
                    Details = Details,
                    Id = Id,
                    InCourt = InCourt ?? 0,
                    LegallyResponsible = LegallyResponsible ?? 0,
                    Preexisting = Preexisting ?? 0,
                    Support = Support ?? 0,
                    UserId = UserId
                };
        }
    }
}
