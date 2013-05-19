using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class CourtViewModel : IViewModel
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int CountyId { get; set; }
        [Display(Name = "Case Number")]
        [RegularExpression(@"^[0-9a-zA-Z\s-]*$", ErrorMessage = "Case Number may only contain numbers, letters and dashes.")]
        public string CaseNumber { get; set; }
        [Required]
        [Display(Name = "Author of Plan")]
        public int AuthorOfPlan { get; set; }
        [Required]
        [Display(Name = "Plan Type")]
        public int PlanType { get; set; }
        [Display(Name = "Case Number")]
        [RegularExpression(@"^[0-9a-zA-Z\s-]*$", ErrorMessage = "Case Number may only contain numbers, letters and dashes.")]
        public string ExistCaseNumber { get; set; }

        public IEnumerable<County> Counties { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<Court>();
        }

    }
}