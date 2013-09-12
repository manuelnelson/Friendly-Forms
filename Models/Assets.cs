using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("Assets")]
    public class Assets : IFormEntity
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
        public int AdditionalAssets { get; set; }
        public string AdditionalAssetsDescription { get; set; }


        public bool IsValid()
        {
            return UserId > 0;
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
