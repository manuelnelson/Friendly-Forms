using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class ExtraDecisionsViewModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ChildId { get; set; }
        [Required]
        [Display(Name = "Decision Maker")]
        public int DecisionMaker { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        public string Description { get; set; }

        public List<ExtraDecisions> ExtraDecisions{ get; set; }

        public ExtraDecisions ConvertToEntity()
        {
            return new ExtraDecisions
                {
                    Id = Id,
                    ChildId = ChildId,
                    DecisionMaker = DecisionMaker,
                    UserId = UserId,
                    Description = Description
                };
        }
    }
}
