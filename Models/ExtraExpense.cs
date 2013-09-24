using System;
using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("ExtraExpenses")]
    public class ExtraExpense : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public User User { get; set; }
        public long ChildId { get; set; }
        [Ignore]
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

        public ExtraExpense ToMonthly()
        {
            return new ExtraExpense
                {
                    Id = Id,
                    UserId = UserId,
                    ChildId = ChildId,
                    TutitionFather = Convert.ToInt32((float)TutitionFather / 12),
                    TutitionMother = Convert.ToInt32((float)TutitionMother/12),
                    TutitionNonParent = Convert.ToInt32((float)TutitionNonParent/12),
                    EducationFather = Convert.ToInt32((float)EducationFather/12),
                    EducationMother = Convert.ToInt32((float)EducationMother/12),
                    EducationNonParent = Convert.ToInt32((float)EducationNonParent/12),
                    MedicalFather = Convert.ToInt32((float)EducationFather/12),
                    MedicalMother = Convert.ToInt32((float)MedicalMother/12),
                    MedicalNonParent = Convert.ToInt32((float)MedicalNonParent/12),
                    SpecialFather = Convert.ToInt32((float)SpecialFather/12),
                    SpecialMother = Convert.ToInt32((float)SpecialMother/12),
                    SpecialNonParent = Convert.ToInt32((float)SpecialNonParent/12),
                    ExtraSpent = Convert.ToInt32((float)ExtraSpent/12),
                };
        }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}