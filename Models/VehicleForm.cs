using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class VehicleForm : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IViewModel ConvertToModel()
        {
            return new VehicleFormViewModel()
            {
                Id = Id,
                VehiclesInvolved = VehiclesInvolved,
                UserId = UserId
            };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (VehicleForm)entity;
            UserId = updatingEntity.UserId;
            VehiclesInvolved = updatingEntity.VehiclesInvolved;
        }

        public int VehiclesInvolved { get; set; }
    }
}
