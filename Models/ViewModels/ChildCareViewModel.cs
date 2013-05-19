using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class ChildCareViewModel : IViewModel
    {
        public long Id { get; set; }
        public int UserId { get; set; }
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
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<ChildCare>();
        }
    }
}
