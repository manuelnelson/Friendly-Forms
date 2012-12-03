using System;
using System.ComponentModel.DataAnnotations;
using Models.Contract;

namespace Models.ViewModels
{
    public class SpecialCircumstancesViewModel : IViewModel
    {
        public int UserId { get; set; }
        public bool IsOtherParent { get; set; }
        public int Circumstances { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Deviation { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Health { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Insurance { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string TaxCredit { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string TravelExpenses { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Visitation { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Alimony { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Mortgage { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string Permanency { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
        public string NonSpecific { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be a number")]
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
