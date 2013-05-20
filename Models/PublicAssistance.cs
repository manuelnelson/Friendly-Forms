using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class PublicAssistance : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
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
