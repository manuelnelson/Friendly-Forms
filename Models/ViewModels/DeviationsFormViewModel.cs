using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class DeviationsFormViewModel : IViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsOtherParent { get; set; }
        public int Deviation { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<ChildForm>();
        }

    }
}
