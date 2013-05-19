using Models.Contract;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class ChildFormViewModel : IViewModel
    {
        public long Id { get; set; }
        public int ChildrenInvolved { get; set; }

        public int UserId { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<ChildForm>();
        }

    }
}
