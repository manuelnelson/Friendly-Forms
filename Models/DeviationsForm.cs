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
        public int? HighLow { get; set; }
        public int? LowDeviation { get; set; }
        public string WhyLow { get; set; }
        public int? HighIncome { get; set; }
        public int? HighDeviation { get; set; }
        public int? NonSpecific { get; set; }
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
