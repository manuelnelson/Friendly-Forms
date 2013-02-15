using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class HealthInsurance : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
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
