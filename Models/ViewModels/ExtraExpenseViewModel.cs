using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class ExtraExpenseViewModel : IViewModel
    {
        public long UserId { get; set; }
        public long ChildId { get; set; }
        public int TutitionFather { get; set; }
        public int TutitionMother { get; set; }
        public int TutitionNonParent { get; set; }
        public int EducationFather { get; set; }
        public int EducationMother { get; set; }
        public int EducationNonParent { get; set; }
        public int MedicalFather { get; set; }
        public int MedicalMother { get; set; }
        public int MedicalNonParent { get; set; }
        public int SpecialFather { get; set; }
        public int SpecialMother { get; set; }
        public int SpecialNonParent { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<ExtraExpense>();
        }
    }
}
