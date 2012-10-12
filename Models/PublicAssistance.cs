using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class PublicAssistance : IFormEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Assistance { get; set; }
        public int OtherAssistance { get; set; }
        public IViewModel ConvertToModel()
        {
            return new PublicAssistanceViewModel()
            {
                Assistance = Assistance,
                UserId = UserId,
                OtherAssistance = OtherAssistance
            };
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
