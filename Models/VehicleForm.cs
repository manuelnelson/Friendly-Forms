using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class VehicleForm : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }

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
