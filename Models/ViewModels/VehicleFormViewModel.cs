using Models.Contract;

namespace Models.ViewModels
{
    public class VehicleFormViewModel : IViewModel
    {
        public int Id { get; set; }
        public int VehiclesInvolved { get; set; }

        public int UserId { get; set; }

        public IFormEntity ConvertToEntity()
        {
            return new VehicleForm()
            {
                Id = Id,
                UserId = UserId,
                VehiclesInvolved = VehiclesInvolved
            };
        }
    }
}
