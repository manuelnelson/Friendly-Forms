using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class VehicleForm : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<VehicleFormViewModel>();
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
