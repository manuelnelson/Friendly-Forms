using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class ChildSupportViewModel : IViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.,'_ \-\s]*$", ErrorMessage = @"Only alpha-numeric characters and [.,_-'] are allowed.")]
        [StringLength(100)]
        [Display(Name = "Paid By")]
        public string PaidBy { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(?!.*--)[A-Za-z0-9\.,'_ \-\s]*$", ErrorMessage = @"Only alpha-numeric characters and [.,_-'] are allowed.")]
        [Display(Name = "Paid To")]
        public string PaidTo { get; set; }
        [Required]
        [Display(Name = "Monthly Amount")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Monthly amount must be a number")]
        public string MonthlyAmount { get; set; }
        [Required]
        [RegularExpression(pattern: @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = @"Date must be in mm/dd/yyyy format")]
        [Display(Name = "Effective Date")]
        public string EffectiveDate { get; set; }
        [Required]
        [Display(Name = "Temporary Agreement")]
        public int TemporaryAgreement { get; set; }
        [Required]
        public int Payment { get; set; }
        [Display(Name = "Payment Day")]
        public int? PaymentDay { get; set; }
        public IFormEntity ConvertToEntity()
        {
            return new ChildSupport()
                {
                    EffectiveDate = Convert.ToDateTime(EffectiveDate),
                    MonthlyAmount = Convert.ToInt32(MonthlyAmount),
                    PaidBy = PaidBy,
                    PaidTo = PaidTo,
                    Payment = Payment,
                    PaymentDay = PaymentDay ?? 0,
                    TemporaryAgreement = TemporaryAgreement,
                    UserId = UserId
                };
        }
    }
}
