using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class ExtraExpenses
    {
        [DataMember]
        public long ChildId { get; set; }
        [DataMember]
        public double TutitionFather { get; set; }
        [DataMember]
        public double TutitionMother { get; set; }
        [DataMember]
        public double TutitionNonParent { get; set; }
        [DataMember]
        public double TutitionNonTotal { get; set; }
        [DataMember]
        public double EducationFather { get; set; }
        [DataMember]
        public double EducationMother { get; set; }
        [DataMember]
        public double EducationNonParent { get; set; }
        [DataMember]
        public double EducationTotal { get; set; }
        [DataMember]
        public double MedicalFather { get; set; }
        [DataMember]
        public double MedicalMother { get; set; }
        [DataMember]
        public double MedicalNonParent { get; set; }
        [DataMember]
        public double MedicalTotal { get; set; }
        [DataMember]
        public double SpecialFather { get; set; }
        [DataMember]
        public double SpecialMother { get; set; }
        [DataMember]
        public double SpecialNonParent { get; set; }
        [DataMember]
        public double SpecialTotal { get; set; }
        [DataMember]
        public double TotalFather { get; set; }
        [DataMember]
        public double TotalMother { get; set; }
        [DataMember]
        public double TotalNonParent { get; set; }
        [DataMember]
        public double TotalTotal { get; set; }
        [DataMember]
        public double ProRataFather { get; set; }
        [DataMember]
        public double ProRataMother { get; set; }
        [DataMember]
        public double ProRataTotal { get; set; }
        [DataMember]
        public double PercentageFather { get; set; }
        [DataMember]
        public double PercentageMother { get; set; }
        [DataMember]
        public double DeviationFather { get; set; }
        [DataMember]
        public double DeviationMother { get; set; }
        [DataMember]
        public string SpecialDescriptionFather { get; set; }
        [DataMember]
        public string SpecialDescriptionMother { get; set; }
        [DataMember]
        public string SpecialDescriptionNonParent { get; set; }
        [DataMember]
        public double ExtraSpent { get; set; }
    }
}