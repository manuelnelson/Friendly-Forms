using Models.Contract;
using Models.ViewModels;

namespace Models
{
    public class Assets : IFormEntity
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int Retirement { get; set; }
        public string RetirementDescription { get; set; }
        public int NonRetirement { get; set; }
        public string NonRetirementDescription { get; set; }
        public int Business { get; set; }
        public string BusinessDescription { get; set; }
        public string AdditionalAssets { get; set; }

        public IViewModel ConvertToModel()
        {
            return new AssetViewModel()
                {
                    BusinessDescription = BusinessDescription,
                    Business = Business,
                    NonRetirement = NonRetirement,
                    NonRetirementDescription = NonRetirementDescription,
                    Retirement = Retirement,
                    RetirementDescription = RetirementDescription,
                    AdditionalAssets = AdditionalAssets,
                    UserId = UserId
                };
        }

        public void Update(IFormEntity entity)
        {
            var updatingEntity = (Assets)entity;
            UserId = updatingEntity.UserId;
            Retirement = updatingEntity.Retirement;
            RetirementDescription = updatingEntity.RetirementDescription;
            NonRetirement = updatingEntity.NonRetirement;
            NonRetirementDescription = updatingEntity.NonRetirementDescription;
            AdditionalAssets = updatingEntity.AdditionalAssets;
            Business = updatingEntity.Business;
            BusinessDescription = updatingEntity.BusinessDescription;
        }
    }
}
