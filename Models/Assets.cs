using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Assets")]
    public class Assets : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int Retirement { get; set; }
        public string RetirementDescription { get; set; }
        public int NonRetirement { get; set; }
        public string NonRetirementDescription { get; set; }
        public int Business { get; set; }
        public string BusinessDescription { get; set; }
        public string AdditionalAssets { get; set; }

        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<AssetViewModel>();
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
