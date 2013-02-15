using Models.Contract;
using ServiceStack.Common.Extensions;

namespace Models.ViewModels
{
    public class VehicleFormViewModel : IViewModel
    {
        public long Id { get; set; }
        public int VehiclesInvolved { get; set; }

        public int UserId { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return this.TranslateTo<VehicleForm>();
        }
    }
}
