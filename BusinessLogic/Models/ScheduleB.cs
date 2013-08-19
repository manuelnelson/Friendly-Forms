using System.Collections.Generic;
using System.Runtime.Serialization;
using Models;

namespace BusinessLogic.Models
{
    [DataContract]
    public class ScheduleB
    {
        [DataMember]
        public int GrossIncome { get; set; }
        [DataMember]
        public int SelfEmploymentIncome { get; set; }
        [DataMember]
        public int FicaIncome { get; set; }
        [DataMember]
        public int MedicareTax { get; set; }
        [DataMember]
        public int Total34 { get; set; }
        [DataMember]
        public int Total5Minus1 { get; set; }
        [DataMember]
        public List<PreexistingSupportChild> PreexistingSupportChild { get; set; }
        [DataMember]
        public List<PreexistingSupport> PreexistingSupport { get; set; }
        [DataMember]
        public int TotalSupport { get; set; }
        [DataMember]
        public int AdjustedSupport { get; set; }

        [DataMember]
        public List<OtherChildDto> OtherChildren { get; set; }
        [DataMember]
        public string OtherChildrenDescription { get; set; }
        [DataMember]
        public int Subtotal { get; set; }
        [DataMember]
        public int GeorgiaObligations { get; set; }
        [DataMember]
        public int TheoreticalSupport { get; set; }
        [DataMember]
        public int PreexistingOrder { get; set; }
    }
}
