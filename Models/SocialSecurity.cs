using Models.Contract;
using ServiceStack.DataAnnotations;


namespace Models
{
    [Alias("SocialSecurities")]
    public class SocialSecurity : IEntity, IFormEntity
    {
        [AutoIncrement]
        public long Id { get; set; }
        public bool IsOtherParent { get; set; }
        public long UserId { get; set; }
        [Ignore]
        public virtual User User { get; set; }
        public int ReceiveSocial { get; set; }
        public int? Amount { get; set; } 

        public bool IsValid()
        {
            return UserId > 0;
        }

        public void Update(IFormEntity entity)
        {
            var updateEntity = (SocialSecurity) entity;
            IsOtherParent = updateEntity.IsOtherParent;
            Amount = updateEntity.Amount;
            ReceiveSocial = updateEntity.ReceiveSocial;
            UserId = updateEntity.UserId;
        }
    }
}
