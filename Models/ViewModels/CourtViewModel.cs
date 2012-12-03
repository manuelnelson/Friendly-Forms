using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class CourtViewModel : IViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CountyId { get; set; }
        [Display(Name = "Case Number")]
        [RegularExpression(@"^[0-9a-zA-Z\s-]*$", ErrorMessage = "Case Number must be alpha-numeric. Dashes are also allowed")]
        public string CaseNumber { get; set; }
        [Required]
        [Display(Name = "Author of Plan")]
        public int AuthorOfPlan { get; set; }
        [Required]
        [Display(Name = "Plan Type")]
        public int PlanType { get; set; }
        [Display(Name = "Case Number")]
        [RegularExpression(@"^[0-9a-zA-Z\s-]*$", ErrorMessage = "Case Number must be alpha-numeric. Dashes are also allowed")]
        public string ExistCaseNumber { get; set; }

        public IEnumerable<County> Counties { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Court
            {
                AuthorOfPlan = AuthorOfPlan,
                CaseNumber = CaseNumber,
                CountyId = CountyId,
                PlanType = PlanType,
                ExistCaseNumber = ExistCaseNumber,
                UserId = UserId
            };
        }

    }
}