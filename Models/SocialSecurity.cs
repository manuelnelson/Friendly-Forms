using Models.Contract;
using Models.ViewModels;
using ServiceStack.Common.Extensions;

namespace Models
{
    public class SocialSecurity : IEntity, IFormEntity
    {
        public long Id { get; set; }
        public bool IsOtherParent { get; set; }
        public long UserId { get; set; }
        public int ReceiveSocial { get; set; }
        public int? Amount { get; set; } 
        public IViewModel ConvertToModel()
        {
            return this.TranslateTo<SocialSecurityViewModel>();
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
