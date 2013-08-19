using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("VehicleForms")]
    public class VehicleForm : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<VehicleFormViewModel>();
        }

        public bool IsValid()
        {
            return UserId > 0;
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
