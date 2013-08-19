using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("SpousalSupports")]
    public class SpousalSupport : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int Spousal { get; set; }
        public string SpousalDescription { get; set; }
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<SpousalViewModel>();
        }

        public bool IsValid()
        {
            return UserId > 0;
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
