using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;

namespace Models
{
    public class ExtraExpense : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long ChildId { get; set; }
        public Child Child { get; set; }
        public int TutitionFather { get; set;}
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
        public string SpecialDescriptionFather { get; set; }
        public string SpecialDescriptionMother { get; set; }
        public string SpecialDescriptionNonParent { get; set; }
        public int ExtraSpent { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ExtraExpenseViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}