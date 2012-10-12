using System;
using Models.Contract;

namespace Models.ViewModels
{
    public class SpecialCircumstancesViewModel : IViewModel
    {
        public int UserId { get; set; }
        public bool IsOtherParent { get; set; }
        public int Circumstances { get; set; }
        public string Deviation { get; set; }
        public string Health { get; set; }
        public string Insurance { get; set; }
        public string TaxCredit { get; set; }
        public string TravelExpenses { get; set; }
        public string Visitation { get; set; }
        public string Alimony { get; set; }
        public string Mortgage { get; set; }
        public string Permanency { get; set; }
        public string NonSpecific { get; set; }
        public string ParentingTime { get; set; }
        public IFormEntity ConvertToEntity()
        {
            return new SpecialCircumstances()
                {
                    IsOtherParent = IsOtherParent,
                    Alimony = Convert.ToInt32(Alimony),
                    Circumstances = Circumstances,
                    Deviation = Convert.ToInt32(Deviation),
                    Health = Convert.ToInt32(Health),
                    Insurance = Convert.ToInt32(Insurance),
                    Mortgage = Convert.ToInt32(Mortgage),
                    NonSpecific = Convert.ToInt32(NonSpecific),
                    ParentingTime = Convert.ToInt32(ParentingTime),
                    Permanency = Convert.ToInt32(Permanency),
                    TaxCredit = Convert.ToInt32(TaxCredit),
                    TravelExpenses = Convert.ToInt32(TravelExpenses),
                    UserId = UserId,
                    Visitation = Convert.ToInt32(Visitation)
                };
        }
    }
}
