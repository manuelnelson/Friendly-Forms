using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class SpecialCircumstances : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public bool IsOtherParent { get; set; }
        public int? Circumstances { get; set; }
        public int? Deviation { get; set; }
        public int? Health { get; set; }
        public int? Insurance { get; set; }
        public int? TaxCredit { get; set; }
        public int? TravelExpenses { get; set; }
        public int? Visitation { get; set; }
        public int? Alimony { get; set; }
        public int? Mortgage { get; set; }
        public int? Permanency { get; set; }
        public int? NonSpecific { get; set; }
        public int? ParentingTime { get; set; }


        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<SpecialCircumstancesViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var update = (SpecialCircumstances)entity;
            IsOtherParent = update.IsOtherParent;
            Alimony = update.Alimony;
            Circumstances = update.Circumstances;
            Deviation = update.Deviation;
            Health = update.Health;
            Insurance = update.Insurance;
            Mortgage = update.Mortgage;
            NonSpecific = update.NonSpecific;
            ParentingTime = update.ParentingTime;
            Permanency = update.Permanency;
            TaxCredit = update.TaxCredit;
            TravelExpenses = update.TravelExpenses;
            UserId = update.UserId;
            Visitation = update.Visitation;
        }
    }
}
