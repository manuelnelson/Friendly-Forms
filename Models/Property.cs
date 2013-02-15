using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class Property : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
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
