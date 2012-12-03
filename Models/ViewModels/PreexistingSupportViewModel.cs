using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class PreexistingSupportViewModel : IViewModel
    {
        public int Id { get; set; }
        public bool IsOtherParent { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Support { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.\?=\+\s.[\]@$'()!~:#&_\%]*$", ErrorMessage = @"Only alpha-numeric characters and []@$'()!~:#&_\% are allowed.")]
        [Display(Name = "Court Name")]
        public string CourtName { get; set; }
        [Required]
        [Display(Name = "Case Number")]
        public string CaseNumber { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        [Display(Name = "Order Date")]
        public string OrderDate { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Monthly { get; set; }

        public List<PreexistingSupport> PreexistingSupportList { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new PreexistingSupport()
                {
                    IsOtherParent = IsOtherParent,
                    CaseNumber = Convert.ToInt32(CaseNumber),
                    CourtName = CourtName,
                    Monthly = Convert.ToInt32(Monthly),
                    OrderDate = Convert.ToDateTime(OrderDate),
                    UserId = UserId,
                    Support = Support,
                    Id = Id
                };
        }
    }
}
