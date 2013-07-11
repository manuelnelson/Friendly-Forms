using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class DeviationsForm : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public int Deviation { get; set; }
        public string Unjust { get; set; }
        public string BestInterest { get; set; }
        public string Impair { get; set; }
        public int? HealthFather { get; set; }
        public int? InsuranceFather { get; set; }
        public int? TaxCreditFather { get; set; }
        public int? TravelExpensesFather { get; set; }
        public int? VisitationFather { get; set; }
        public int? AlimonyPaidFather { get; set; }
        public int? MortgageFather { get; set; }
        public int? PermanencyFather { get; set; }
        public int? NonSpecificFather { get; set; }
        public int? HealthMother { get; set; }
        public int? InsuranceMother { get; set; }
        public int? TaxCreditMother { get; set; }
        public int? TravelExpensesMother { get; set; }
        public int? VisitationMother { get; set; }
        public int? AlimonyPaidMother { get; set; }
        public int? MortgageMother { get; set; }
        public int? PermanencyMother { get; set; }
        public int? NonSpecificMother { get; set; }
        public int? HighLow { get; set; }
        public int? LowDeviation { get; set; }
        public string WhyLow { get; set; }
        public int? HighIncome { get; set; }
        public int? HighDeviation { get; set; }
        public int? SpecificDeviations { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<DeviationsFormViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
