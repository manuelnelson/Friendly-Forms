using System.ComponentModel.DataAnnotations;
using Models.Contract;

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

        public IFormEntity ConvertToEntity()
        {
            return new Decisions()
                {
                    ChildId = ChildId,
                    Education = Education,
                    ExtraCurricular = ExtraCurricular,
                    HealthCare = HealthCare,
                    Religion = Religion,
                    UserId = UserId
                };
        }
    }
}
