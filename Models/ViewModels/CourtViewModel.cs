using System;
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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Case must be a number")]
        public string CaseNumber { get; set; }
        [Required]
        [Display(Name = "Author of Plan")]
        public int AuthorOfPlan { get; set; }
        [Required]
        [Display(Name = "Plan Type")]
        public int PlanType { get; set; }
        [Display(Name = "Plan Date")]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        public string PlanDate { get; set; }

        public IEnumerable<County> Counties { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new Court
            {
                AuthorOfPlan = this.AuthorOfPlan,
                CaseNumber = Convert.ToInt16(this.CaseNumber),
                CountyId = this.CountyId,
                PlanType = this.PlanType,
                PlanDate = string.IsNullOrEmpty(PlanDate) ? (DateTime?)null : Convert.ToDateTime(PlanDate),
                UserId = this.UserId
            };
        }

    }
}