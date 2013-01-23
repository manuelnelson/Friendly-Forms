using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class SpousalSupport : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int Spousal { get; set; }
        public string SpousalDescription { get; set; }
        public IViewModel ConvertToModel()
        {
            return new SpousalViewModel()
                {
                    SpousalDescription = SpousalDescription,
                    Spousal = Spousal,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (SpousalSupport)entity;
            UserId = updatingEntity.UserId;
            Spousal = updatingEntity.Spousal;
            SpousalDescription = updatingEntity.SpousalDescription;
        }
    }
}
