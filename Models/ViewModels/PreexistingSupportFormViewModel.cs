using Models.Contract;
using ServiceStack.Common;
using ServiceStack.Common;

namespace Models.ViewModels
{
    public class PreexistingSupportFormViewModel : IViewModel
    {
        public long Id { get; set; }
        public bool IsOtherParent { get; set; }
        public long UserId { get; set; }
        public int Support { get; set; }
        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<PreexistingSupportForm>();
        }
    }
}