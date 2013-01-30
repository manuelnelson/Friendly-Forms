using Models.ViewModels;

namespace BusinessLogic.Models
{
    public class AllFinancialViewModel
    {
        public IncomeViewModel IncomeViewModel { get; set; }
        public SocialSecurityViewModel SocialSecurityViewModel { get; set; }
        public AllPreexistingViewModel PreexistingSupportViewModel { get; set; }
        public OtherChildrenViewModel OtherChildrenViewModel { get; set; }
        public SpecialCircumstancesViewModel SpecialCircumstancesViewModel { get; set; }
        public IncomeViewModel IncomeOtherViewModel { get; set; }
        public SocialSecurityViewModel SocialSecurityOtherViewModel { get; set; }
        public AllPreexistingViewModel PreexistingSupportOtherViewModel { get; set; }
        public OtherChildrenViewModel OtherChildrenOtherViewModel { get; set; }
        public SpecialCircumstancesViewModel SpecialCircumstancesOtherViewModel { get; set; }
        public FinancialFormsCompleted FinancialFormsCompleted { get; set; }
        public int FormUserId { get; set; }
    }

    public class FinancialFormsCompleted
    {
        public bool Income { get; set; }
        public bool SocialSecurity { get; set; }
        public bool Preexisting { get; set; }
        public bool OtherChildren { get; set; }
        public bool SpecialCircumstance { get; set; }
        public bool IncomeOther { get; set; }
        public bool SocialSecurityOther { get; set; }
        public bool PreexistingOther { get; set; }
        public bool OtherChildrenOther { get; set; }
        public bool SpecialCircumstanceOther { get; set; }
    }
}
