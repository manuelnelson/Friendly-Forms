using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class ScheduleB
    {
        [DataMember]
        public double GrossIncome { get; set; }
        [DataMember]
        public double SelfEmploymentIncome { get; set; }
        [DataMember]
        public double FicaIncome { get; set; }
        [DataMember]
        public double MedicareTax { get; set; }
        [DataMember]
        public double Total34 { get; set; }
        [DataMember]
        public double Total5Minus1 { get; set; }
        //[DataMember]
        //public List<PreexistingSupportChild> PreexistingSupportChild { get; set; }
        //[DataMember]
        //public List<PreexistingSupport> PreexistingSupport { get; set; }
        [DataMember]
        public double TotalSupport { get; set; }
        [DataMember]
        public double AdjustedSupport { get; set; }

        [DataMember]
        public List<OtherChildDto> OtherChildren { get; set; }
        [DataMember]
        public string OtherChildrenDescription { get; set; }
        [DataMember]
        public double Subtotal { get; set; }
        [DataMember]
        public double GeorgiaObligations { get; set; }
        [DataMember]
        public double TheoreticalSupport { get; set; }
        [DataMember]
        public double PreexistingOrder { get; set; }
        [DataMember]
        public string IncomeDetails { get; set; }
    }
}
