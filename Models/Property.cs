using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Properties")]
    public class Property : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int RealEstate { get; set; }
        public string RealEstateDescription { get; set; }
        public int PersonalProperty { get; set; }
        public string DividingProperty { get; set; }

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Property)entity;
            UserId = updatingEntity.UserId;
            RealEstate = updatingEntity.RealEstate;
            RealEstateDescription = updatingEntity.RealEstateDescription;
            PersonalProperty = updatingEntity.PersonalProperty;
            DividingProperty = updatingEntity.DividingProperty;
        }
    }
}
