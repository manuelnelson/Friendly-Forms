using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;


namespace Models
{
    public class Property : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public int RealEstate { get; set; }
        public string RealEstateDescription { get; set; }
        public int PersonalProperty { get; set; }
        public string DividingProperty { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<PropertyViewModel>();
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
