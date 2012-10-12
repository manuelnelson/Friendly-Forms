using System;
using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class SpecialCircumstances : IFormEntity
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual bool IsOtherParent { get; set; }
        public virtual int Circumstances { get; set; }
        public virtual int Deviation { get; set; }
        public virtual int Health { get; set; }
        public virtual int Insurance { get; set; }
        public virtual int TaxCredit { get; set; }
        public virtual int TravelExpenses { get; set; }
        public virtual int Visitation { get; set; }
        public virtual int Alimony { get; set; }
        public virtual int Mortgage { get; set; }
        public virtual int Permanency { get; set; }
        public virtual int NonSpecific { get; set; }
        public virtual int ParentingTime { get; set; }


        public IViewModel ConvertToModel()
        {
            return new SpecialCircumstancesViewModel()
                {
                    IsOtherParent = IsOtherParent,
                    Alimony = Alimony.ToString(),
                    Circumstances = Circumstances,
                    Deviation = Deviation.ToString(),
                    Health = Health.ToString(),
                    Insurance = Insurance.ToString(),
                    Mortgage = Mortgage.ToString(),
                    NonSpecific = NonSpecific.ToString(),
                    ParentingTime = ParentingTime.ToString(),
                    Permanency = Permanency.ToString(),
                    TaxCredit = TaxCredit.ToString(),
                    TravelExpenses = TravelExpenses.ToString(),
                    UserId = UserId,
                    Visitation = Visitation.ToString()
                };
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
