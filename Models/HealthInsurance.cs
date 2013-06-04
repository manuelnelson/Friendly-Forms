using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("HealthInsurances")]
    public class HealthInsurance : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
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
