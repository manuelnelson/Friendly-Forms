using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class HealthInsurance : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public int Health { get; set; }
        public string HealthDescription { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<HealthInsuranceViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (HealthInsurance)entity;
            UserId = updatingEntity.UserId;
            Health = updatingEntity.Health;
            HealthDescription = updatingEntity.HealthDescription;
        }
    }
}
