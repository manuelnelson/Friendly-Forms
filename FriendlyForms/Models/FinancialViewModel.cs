using BusinessLogic.Models;
using Models.ViewModels;

namespace FriendlyForms.Models
{
    public class FinancialViewModel
    {
        public IncomeViewModel IncomeViewModel { get; set; }
        public SocialSecurityViewModel SocialSecurityViewModel { get; set; }
        public AllPreexistingViewModel PreexistingSupportViewModel { get; set; }
        public OtherChildrenViewModel OtherChildrenViewModel { get; set; }
        public DeviationsFormViewModel DeviationsFormViewModel { get; set; }
        public IncomeViewModel IncomeOtherViewModel { get; set; }
        public SocialSecurityViewModel SocialSecurityOtherViewModel { get; set; }
        public AllPreexistingViewModel PreexistingSupportOtherViewModel { get; set; }
        public OtherChildrenViewModel OtherChildrenOtherViewModel { get; set; }
        public HealthViewModel HealthViewModel { get; set; }
        public ExtraExpenseFormViewModel ExtraExpenseFormViewModel { get; set; }
        public ChildCareFormViewModel ChildCareFormViewModel { get; set; }
        public FinancialFormsCompleted FinancialFormsCompleted { get; set; }
        public ChildAllViewModel ChildAllViewModel { get; set; }
        public DeviationsFormViewModel DeviationsOtherFormViewModel { get; set; }
    }

    public class FinancialFormsCompleted
    {
        public bool Deviation { get; set; }
        public bool Income { get; set; }
        public bool ChildCareForm { get; set; }
        public bool SocialSecurity { get; set; }
        public bool Preexisting { get; set; }
        public bool OtherChildren { get; set; }
        public bool Health { get; set; }
        public bool IncomeOther { get; set; }
        public bool SocialSecurityOther { get; set; }
        public bool PreexistingOther { get; set; }
        public bool OtherChildrenOther { get; set; }
        public bool ExtraExpenses { get; set; }
        public bool DeviationOther { get; set; }
    }
}
