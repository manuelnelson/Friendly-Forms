using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class ChildCareFormViewModel : IViewModel
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
