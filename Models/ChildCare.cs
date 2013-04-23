using System;
using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class ChildCare : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int ChildId { get; set; }
        public int SchoolFather { get; set; }
        public int SchoolMother { get; set; }
        public int SchoolNonParent { get; set; }
        public int SummerFather { get; set; }
        public int SummerMother { get; set; }
        public int SummerNonParent { get; set; }
        public int BreaksFather { get; set; }
        public int BreaksMother { get; set; }
        public int BreaksNonParent { get; set; }
        public int OtherFather { get; set; }
        public int OtherMother { get; set; }
        public int OtherNonParent { get; set; }
        public User User { get; set; }
        public Child Child { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<ChildCareViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
