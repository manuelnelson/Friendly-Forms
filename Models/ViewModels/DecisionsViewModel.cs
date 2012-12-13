using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class DecisionsViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ChildId { get; set; }
        [Required]
        public int Education { get; set; }
        [Required]
        [Display(Name = "Health Care")]
        public int HealthCare { get; set; }
        [Required]
        public int Religion { get; set; }
        [Required]
        [Display(Name = "Extra Curricular")]
        public int ExtraCurricular { get; set; }

        [Display(Name = "Resolve")]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#/&_\-,\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_,/-?\% are allowed.")]
        public string BothResolve { get; set; }
        
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Decisions>();
            //return new Decisions()
            //    {
            //        ChildId = ChildId,
            //        Education = Education,
            //        ExtraCurricular = ExtraCurricular,
            //        HealthCare = HealthCare,
            //        Religion = Religion,
            //        UserId = UserId
            //    };
        }
    }
}
