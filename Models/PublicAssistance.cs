using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("PublicAssistances")]
    public class PublicAssistance : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int Assistance { get; set; }
        public int OtherAssistance { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<PublicAssistanceViewModel>();
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (PublicAssistance) entity;
            Assistance = updatingEntity.Assistance;
            UserId = updatingEntity.UserId;
            OtherAssistance = updatingEntity.OtherAssistance;
        }
    }
}
